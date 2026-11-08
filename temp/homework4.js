// // # 1

// let text = "hello@gmail.com";

// let pattern = /\w+@[a-zA-Z]+.com/;

// console.log(pattern.test(text));

// // # 2

// let text = "https://uk.wikipedia.org/wiki/";

// let pattern = /https:\/\/[\w-_.~/]+/;

// console.log(pattern.test(text));

// // # 3

// let text = `
//     <html lang="en">
//     <head>
//         <meta charset="UTF-8">
//         <meta name="viewport" content="width=device-width, initial-scale=1.0">
//         <title>Document</title>
//     </head>
//     <body>
//         <script>
//             document.write("Мій перший JS код!")
//         </script>
//     </body>
//     </html>
// `

// let pattern = /(<[^>]+>)/g;

// console.log(text.match(pattern));

// // # 4

// let text = `
//     <html lang="en">
//     <head>
//         <meta charset="UTF-8">
//         <meta name="viewport" content="width=device-width, initial-scale=1.0">
//         <title>Document</title>
//     </head>
//     <body>
//         <a>
//         </a>
//     </body>
//     </html>
// `

// let pattern = /<a>[\s\S]*?<\/a>/;

// console.log(text.replace(pattern,''));

// // # 5

// let text = "#RRGGBB";

// let pattern = /^[#][\w]{2}/g;

// console.log(text.match(pattern));

// // # 6

// let text = "Hello Яблуко Київ Jason";

// let pattern = /\p{Script=Latin}+/gu;

// console.log(text.match(pattern));

// // # 7

// let text = "Hello Яблуко Київ Jason";

// let pattern = /\p{Script=Cyrillic}+/gu;

// console.log(text.match(pattern));

// // # 8

// let text = "153.35.244.123";

// let pattern = /((25[1-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?).)+/g;

// console.log(pattern.test(text));

// // # 9

// let text =  "Hello, World! Hello Mother!";

// let filterText = text.replace(/[,!]/g,'').split(' ');

// let repeatedWords = filterText.reduce((previous,current,index) =>
//     filterText.some((element, indexLesser) => element == current && indexLesser != index) 
//     ? 
//         (previous.some(element => element == current) ? previous : (previous.push(current),previous))
//     : previous, []);

// console.log(repeatedWords);

// // # 10

// let text =  "pass2Wo0rd!";

// let pattern = /(.*?[a-z].*?)(.*?[A-Z].*?)(.*?[\d].*?)(.*?[\W].*?)/;

// console.log(pattern.test(text));







