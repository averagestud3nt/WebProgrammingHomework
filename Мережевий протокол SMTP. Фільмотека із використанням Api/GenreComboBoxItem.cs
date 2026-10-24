using System;
using System.Collections.Generic;
using System.Text;

public class GenreComboBoxItem
{
    public string Text { get; set; }
    public int Value { get; set; }

    public override string ToString()
    {
        return Text;
    }
}
