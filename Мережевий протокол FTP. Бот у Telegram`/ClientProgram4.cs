using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
class Program
{
    static List<Quiz> quizzes = new List<Quiz> 
    { 
        new Quiz(){id = 1, questionsSet = new List<Quiz.Question>
            {
                new Quiz.Question() 
                {
                    id = 1,question = "Question",
                    answers = new List<Quiz.Answers>
                    {
                        new Quiz.Answers(){text = "answer1", callbackData = "1", explenation = "explenation1", isCorrectAnswer = false},
                        new Quiz.Answers(){text = "answer2", callbackData = "2", explenation = "explenation2", isCorrectAnswer = false},
                        new Quiz.Answers(){text = "answer3", callbackData = "3", explenation = "explenation3", isCorrectAnswer = false},
                        new Quiz.Answers(){text = "answer4correct", callbackData = "4", explenation = "explenation4", isCorrectAnswer = true},
                    }
                },
                new Quiz.Question()
                {
                    id = 1,question = "Question",
                    answers = new List<Quiz.Answers>
                    {
                        new Quiz.Answers(){text = "answer1-1", callbackData = "1", explenation = "explenation1", isCorrectAnswer = false},
                        new Quiz.Answers(){text = "answer2-2", callbackData = "2", explenation = "explenation2", isCorrectAnswer = false},
                        new Quiz.Answers(){text = "answer3-3", callbackData = "3", explenation = "explenation3", isCorrectAnswer = false},
                        new Quiz.Answers(){text = "answer4-4correct", callbackData = "4", explenation = "explenation4", isCorrectAnswer = true},
                    }
                }
            } 
        },
    };

    static Quiz selectedQuiz{ get; set; }
    static int currentQuestionSetId;
    static async Task Main()
    {
        using var cts = new CancellationTokenSource();
        var botClient = new TelegramBotClient("Your-Token", cancellationToken: cts.Token);

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>() // отримуємо всі типи оновлень
        };

        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

        var me = await botClient.GetMe();
        Console.WriteLine($"Bot under name @{me.Username}, started.");
        Console.ReadLine();
        cts.Cancel();
    }

    static int gameStage = 0;
    static int counter = 0;
    static InlineKeyboardMarkup QuestionBoard = new();

    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            await HandleCallbackQuery(botClient, update.CallbackQuery);
        }
        if (update.Message is not { } message)
            return;
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        Console.WriteLine($"Recieved message: '{messageText}', in chat: {chatId}.");

        Message sentMessage = new Message();

        switch(gameStage)
        {
            case 0:

                if (messageText != "/start")
                {
                    sentMessage = await botClient.SendMessage(
                        chatId: chatId,
                        text: "Type /start to start the Quiz!",
                        cancellationToken: cancellationToken
                    );
                }
                else if (messageText == "/start")
                {
                    sentMessage = await botClient.SendMessage(
                        chatId: chatId,
                        text: "Let the quiz begin!",
                        cancellationToken: cancellationToken
                    );

                    counter = 0;
                    gameStage = 1;
                    selectedQuiz = quizzes[Random.Shared.Next(0, quizzes.Count)];
                }
                break;
            case 1:
                if (selectedQuiz.questionsSet.Count == 0)
                {
                    gameStage = 0;
                    break;
                }
                if (counter == selectedQuiz.questionsSet.Count)
                {
                    gameStage = 0;
                    sentMessage = await botClient.SendMessage(
                        chatId: chatId,
                        text: "Quiz finished!",
                        cancellationToken: cancellationToken
                    );
                    break;
                }
                if (counter < selectedQuiz.questionsSet.Count)
                {
                    QuestionBoard = new InlineKeyboardMarkup();
                    for (int i = 0; i < selectedQuiz.questionsSet[counter].answers.Count; i++)
                    {
                        var button = InlineKeyboardButton.WithCallbackData(selectedQuiz.questionsSet[counter].answers[i].text, selectedQuiz.questionsSet[counter].id + ":" + selectedQuiz.questionsSet[counter].answers[i].callbackData);
                        if ( (i + 1) % 2 != 0)
                        {
                            QuestionBoard.AddNewRow();
                        }
                        QuestionBoard.AddButton(button);
                    }
                    counter++;
                }

                sentMessage = await botClient.SendMessage(
                chatId: chatId,
                text: $"{selectedQuiz.questionsSet[counter - 1].question}",
                replyMarkup: QuestionBoard,
                cancellationToken: cancellationToken);
                gameStage = 2;

                break;
            case 2:
                sentMessage = await botClient.SendMessage(
                    chatId: chatId,
                    text: "Tap/Press the box with the correct answer(s)!",
                    cancellationToken: cancellationToken
                );
                break;
        }
    }

    static async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        if (gameStage == 2)
        {
            var content = callbackQuery.Data.Split(":");
            var checkedQuestionSet = selectedQuiz.questionsSet.FirstOrDefault((e => e.id.ToString() == content[0]));
            var checkedAnswers = checkedQuestionSet.answers;
            if (checkedAnswers.FirstOrDefault(e=>e.callbackData == content[1]).isCorrectAnswer)
            {
                string reply = "";
                foreach (var item in checkedAnswers)
                {
                    reply += $"{item.text}: {item.explenation}\n";
                }
                await botClient.SendMessage(callbackQuery.Message.Chat.Id, $"Correct! Explenations:\n" + reply);
            }
            else
            {
                string reply = "";
                foreach (var item in checkedAnswers)
                {
                    reply += $"{item.text}: {item.explenation}\n";
                }
                await botClient.SendMessage(callbackQuery.Message.Chat.Id, $"Incorrect! Explenations:\n" + reply);
            }
            gameStage = 1;
            return;
        }
    }

    static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}

public class Quiz
{
    public int id;
    public List<Question> questionsSet;
    public struct Question
    {
        public int id;
        public string question;
        public List<Answers> answers;
    }
    public struct Answers
    {
        public string text;
        public string callbackData;
        public bool isCorrectAnswer;
        public string explenation;
    }
}