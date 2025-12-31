using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;

namespace animals;

public partial class MainWindow : Window
{
    DispatcherTimer timer = new DispatcherTimer();
    int tenthsOfSecondsElapsed;
    int matchesFound;
    TextBlock? lastTextBlockClicked;
    bool findingMatch = false;
    public MainWindow()
    {
        InitializeComponent();

        timer.Interval = TimeSpan.FromSeconds(.1);
        timer.Tick += Timer_Tick;
        SetUpGame();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        tenthsOfSecondsElapsed++;
        timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
        if (matchesFound == 8)
        {
            timer.Stop();
            timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
        }
    }

    private void SetUpGame()
    {
        tenthsOfSecondsElapsed = 0;
        matchesFound = 0;
        timeTextBlock.Text = "0.0s";
        List<string> animalEmoji = new List<string>()
        {
            "🦈","🦈",
            "🐙","🐙",
            "🐚","🐚",
            "🐌","🐌",
            "🐤","🐤",
            "🐢","🐢",
            "🐊","🐊",
            "🐅","🐅"
        };

        Random random = new Random();

        foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
        {
            if (textBlock.Name != "timeTextBlock")
            {
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                textBlock.IsVisible = true;
                animalEmoji.RemoveAt(index);
            }
        }
        timer.Start();
    }

    private void TextBlock_MouseDown(object sender, PointerPressedEventArgs e)
    {
        TextBlock? textBlock = sender as TextBlock;
        if (textBlock == null) return;
        if (findingMatch == false)
        {
            textBlock.IsVisible = false;
            lastTextBlockClicked = textBlock;
            findingMatch = true;
        }
        else if (textBlock.Text == lastTextBlockClicked?.Text)
        {
            textBlock.IsVisible = false;
            matchesFound++;
            findingMatch = false;
        }
        else
        {
            lastTextBlockClicked!.IsVisible = true;
            findingMatch = false;
        }
    }

    private void TimeTextBlock_MouseDown(object sender, PointerPressedEventArgs e)
    {
        if (matchesFound == 8)
        {
            SetUpGame();
        }
    }   
}