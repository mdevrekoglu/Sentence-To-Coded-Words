using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_3_Trial_2
{
    class Program
    {
        static void Main()
        {
            bool stop = true, next_word = false;
            string sentence, input, temp_input;
            int word_counter, star_counter = 0;
            int type = 0;

            //The part that takes sentence from user.
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to word game!");
                Console.Write("Please enter a sentence: ");
                sentence = Console.ReadLine();
                for (int i = 0; i < sentence.Length; i++)
                {
                    if (!(sentence[i] == 44 || sentence[i] == 46 || (sentence[i] >= 46 && sentence[i] <= 90) || (sentence[i] >= 97 && sentence[i] <= 122) || sentence[i] == 32))//Checks if the letters are according to the rules.
                    {
                        stop = false;
                        break;
                    }
                    else
                        stop = true;
                }
                if (sentence.Any(c => char.IsDigit(c)) == true)//Checks if there is any number.
                    stop = false;
            } while (!stop);

            //Organises the sentence
            sentence = sentence.Trim().Replace(",", " ");
            sentence = sentence.Trim().Replace(".", " ");
            while (sentence.Contains("  "))
                sentence = sentence.Trim().Replace("  ", " ");
            string[] words_1 = sentence.Split(' ');
            words_1 = words_1.Distinct().ToArray();
            //Creates a new new array and lowers the words.
            string[] words_1_lower = new string[words_1.Length];
            for (int i = 0; i < words_1.Length; i++)
            {
                words_1_lower[i] = words_1[i].ToLower();
            }

            //The part that takes coded word from user.        
            do
            {
                Console.Clear();
                Console.Write("Please enter a coded word: ");
                input = Console.ReadLine();
                input = input.Trim().Replace(" ", "");
                input = input.ToLower();
                for (int i = 0; i < input.Length; i++)
                {
                    //Checks if the letters are according to the rules.
                    if (!(input[i] == 42 || input[i] == 45 || (input[i] >= 97 && input[i] <= 122)))
                    {
                        stop = false;
                        break;
                    }
                    else
                        stop = true;

                    //It detects the type of coded word.
                    if (type == 0 && input[i] == 42)//*
                        type = 1;
                    else if (type == 0 && input[i] == 45)//-
                        type = 2;
                    else if (type == 1 && input[i] == 45)
                    {
                        stop = false;
                        break;
                    }
                    else if (type == 2 && input[i] == 42)
                    {
                        stop = false;
                        break;
                    }
                }
                if (type == 0)//If there is no symbol or there is two symbol, it works. 
                    stop = false;
                if (input.Any(c => char.IsDigit(c)))
                    stop = false;
            } while (!stop);


            if (type == 1)
            {
                while (input.Contains("**"))
                    input = input.Trim().Replace("**", "*");

                //Counts the number of starts in a coded word
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == 42)
                        star_counter++;
                }
                //Creates spaces for right and left side of star. And it stores Indexes of word
                int[] inputarr = new int[star_counter + 2];

                //It changes the words of text.
                for (int u = 0; u < words_1_lower.Length; u++)
                {
                    for (int i = 0; i < inputarr.Length; i++)//It resets the array.
                        inputarr[i] = 0;
                    inputarr[0] = -1;//It provides to check from 0.
                    star_counter = 1;
                    temp_input = "";
                    //It changes the letters of coded word.
                    for (int o = 0; o < input.Length; o++)
                    {
                        if (input[o] != 42)//It creates a temproary words which is the word between two stars.
                            temp_input += input[o];

                        if (input[o] == 42 || o == input.Length - 1)//It works when there is a star or it is the last time that for loop works.
                        {
                            //If the first letter is not a star it checks if word starts with the first part of coded word.
                            if (star_counter == 1 && words_1_lower[u].IndexOf(temp_input, inputarr[1]) != 0)
                                break;
                            else if (input[o] != 42 && o == input.Length - 1 && words_1_lower[u].IndexOf(temp_input, inputarr[star_counter - 1] + 1) != words_1_lower[u].Length - temp_input.Length)
                                //If it is the last time it checks if the last part of word and coded word are same.
                                break;

                            //If new part is index of word it stores the index.
                            if (words_1_lower[u].IndexOf(temp_input, inputarr[star_counter - 1] + 1) != -1)
                                inputarr[star_counter] = words_1_lower[u].IndexOf(temp_input, inputarr[star_counter - 1] + 1) + temp_input.Length - 1;
                            else
                                break;

                            temp_input = "";

                            //It checks if the new index is lower than the others.
                            for (int i = 1; i < star_counter; i++)
                            {
                                if (inputarr[star_counter] < inputarr[i])
                                {
                                    next_word = true;
                                    break;
                                }
                            }

                            //If everything is fine word is being written.
                            if (o == input.Length - 1 && next_word == false)
                                Console.WriteLine(words_1[u]);

                            //It stops the for loop and provides us to choose next word.
                            if (next_word == true)
                            {
                                next_word = false;
                                break;
                            }

                            //It means we are on the next part of the coded word.
                            star_counter++;
                        }
                    }
                }
            }
            else if (type == 2)//It checks if coded word is matching with word.
            {
                word_counter = input.Length;
                for (int u = 0; u < words_1_lower.Length; u++)
                {
                    if (words_1[u].Length == word_counter)
                    {
                        for (int o = 0; o < word_counter; o++)
                        {
                            if ((input[o] != 45) && (input[o] != words_1_lower[u][o]))
                            {
                                break;
                            }
                            if (word_counter - 1 == o)
                                Console.WriteLine(words_1[u]);
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
}