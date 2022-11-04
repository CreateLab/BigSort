using System.Text;

namespace Generator;

public class Gen
{
    public static void Run(string[] args)
    {
        var randomPhrases = new string[]
        {
            "Hello World!",
            "Greetings Earthling!",
            "Greetings Program!",
            "Greetings Human!",
            "Greetings Humanoid!",
            "Apple",
            "Banana",
            "Orange",
            "Cherry is the best",
            "Strawberry is the best",
            "Blueberry is the best",
            "Raspberry is the best",
            "Something something something dark side",
            "Something something something light side",
            "Altium is my target",
            "Altium is my friend",
            "Altium is my enemy",
            "Altium is my everything",
            "Altium is my life",
            "Altium is my love",
            "Altium is my passion",
            "Altium is my hobby",
            "Altium is my everything",
            "На входе есть большой текстовый файл, где каждая строка имеет вид Number. String",
            "На выходе нужно получить файл, где каждая строка имеет вид String. Number",
            "Все строки в файле должны быть отсортированы по алфавиту",
            "Все строки в файле должны быть отсортированы по алфавиту в обратном порядке",
            "Все строки в файле должны быть отсортированы по длине",
            "Все строки в файле должны быть отсортированы по длине в обратном порядке",
            "Обе части могут в пределах файла повторяться",
            "Критерий сортировки: сначала сравнивается часть String, если она совпадает, тогда Number",
            "Пусть к нормальному косметологу сходит",
            "Диплом врача на месте отобрать, идёт работать",
            "Ойтишница, попрошу. Не надо путать.",
            "Пошли в магазин, купили молоко, вернулись домой",
            "у него на обувной полочке",
        };

        var fileSize = int.Parse(args[1]) * 1024 * 1024;

        var fileName = args[0];

        using var fileStream = new FileStream(fileName, FileMode.Create);
        while (fileSize > fileStream.Length)
        {
            var firstPhrase = randomPhrases[new Random().Next(0, randomPhrases.Length)];
            var secondPhrase = randomPhrases[new Random().Next(0, randomPhrases.Length)];
            var line = $"{new Random().Next(0, 1000)}. {firstPhrase} {secondPhrase} " + Environment.NewLine;
            fileStream.Write(Encoding.UTF8.GetBytes(line));
        }
    }
}