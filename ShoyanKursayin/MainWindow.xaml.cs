﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace ShoyanKursayin
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Fill inst;
		public static string Lang { get; set; } = "RU";
		public MainWindow()
		{

			InitializeComponent();
		}


		string ans;
		string questio;
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			inst = new Fill(this);
			questionBox.Focus();
			XmlDocument doc = new XmlDocument();
			doc.Load("logsHistory.xml");
			XmlElement conv = doc.CreateElement("Conversation");
			conv.SetAttribute("date", DateTime.Now.ToLongDateString());
			conv.SetAttribute("time", DateTime.Now.ToLongTimeString());
			XmlNode root = doc.ChildNodes.Item(1);
			root.PrependChild(conv);
			doc.Save("logsHistory.xml");
		}
		private void FillIsTypeing(object answer)
		{
			this.Dispatcher.Invoke(() =>
			{
				ans = inst.GetAnswer(answer.ToString());
			});
			Stopwatch stopwatch = new Stopwatch();
			int time = Convert.ToInt32(ans.Length * 30);
			for (int i = 0; i < 5; i++)
			{
				stopwatch.Restart();
				do
				{
					this.Dispatcher.Invoke(() =>
					{

						answerBlock.Text = "Филл Печатает";

						if (stopwatch.ElapsedMilliseconds > 100)
						{
							answerBlock.Text = "Филл Печатает";
						}
						if (stopwatch.ElapsedMilliseconds > 300)
						{
							answerBlock.Text = "Филл Печатает.";
						}
						if (stopwatch.ElapsedMilliseconds > 500)
						{
							answerBlock.Text = "Филл Печатает..";
						}
						if (stopwatch.ElapsedMilliseconds > 700)
						{
							answerBlock.Text = "Филл Печатает...";
						}


					});
				} while (stopwatch.ElapsedMilliseconds <= time);
			}
			stopwatch.Stop();
			this.Dispatcher.Invoke(() =>
			{
				//answerBlock2.Text = String.Empty;
				hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, ans);
				hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, new string(' ', 10));
				hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, "\n");
				//answerBlock2.Text = hystoryTextBlock.Text.Insert(0, questionBox.Text);
				hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, answer.ToString());
				hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, "\n");
				answerBlock.Text = ans;
				questionBox.IsEnabled = true;
				questionBox.Focus();
			});
		}
		int count;
		System.Timers.Timer timer = new System.Timers.Timer();

		private void TextBox1_PrevKeyDown(object sender, KeyEventArgs e)
		{

			if (e.Key == Key.Return)
			{

				if (questionBox.Text.ToLower() == "пока" || questionBox.Text.ToLower() == "прощай")
				{
					MainWindow context = this;
					inst.Close();

					answerBlock.Text = "Прощай";
				}
				else if (questionBox.Text.Length > 0)
				{
					questionBox.IsEnabled = false;
					
					inst.AllPrevQuestionList.Add(questionBox.Text);
					inst.pos = inst.AllPrevQuestionList.Count;
					timer = new System.Timers.Timer();
					questio = questionBox.Text;
					ans = inst.GetAnswer(questionBox.Text);
					count = Convert.ToInt32(ans.Length / 3);
					if (count > 30)
					{
						count = 20;
					}
					answerBlock.Text = "Филл Печатает";
					timer.Interval = 200;
					timer.Elapsed += new ElapsedEventHandler(Timer_Tick);
					timer.Enabled = true;
					timer.Start();

					#region XMLWrite
					XmlDocument doc = new XmlDocument();
					doc.Load("logsHistory.xml");
					XmlElement rep = doc.CreateElement("Replica");
					XmlElement quest = doc.CreateElement("Question");
					XmlElement answ = doc.CreateElement("Answer");
					quest.InnerText = questionBox.Text;
					answ.InnerText = ans;
					rep.AppendChild(quest);
					rep.AppendChild(answ);
					XmlNodeList convs = doc.GetElementsByTagName("Conversation");
					XmlNode last = convs.Item(0);
					last.AppendChild(rep);
					doc.Save("logsHistory.xml");
					#endregion

					questionBox.Text = String.Empty;
				}

			}
			else if (e.Key == Key.Up)
			{
				if (inst.pos > 0)
				{
					inst.pos--;
					questionBox.Text = inst.AllPrevQuestionList[inst.pos];
				}

			}
			else if (e.Key == Key.Down)
			{
				if (inst.pos < inst.AllPrevQuestionList.Count - 1)
				{
					inst.pos++;
					questionBox.Text = inst.AllPrevQuestionList[inst.pos];
				}
				else
				{
					inst.pos = inst.AllPrevQuestionList.Count;
					questionBox.Text = String.Empty;
				}
			}
		}
		void Timer_Tick(object sender, EventArgs e)
		{
			this.Dispatcher.Invoke(() =>
			{
				count--;
				if (count == 0)
				{
					hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, ans);
					hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, new string(' ', 10));
					hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, "\n");
					//answerBlock2.Text = hystoryTextBlock.Text.Insert(0, questionBox.Text);
					hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, questio);
					hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, "\n");
					answerBlock.Text = ans;
					questionBox.IsEnabled = true;
					questionBox.Focus();
					timer.Stop();
				}
				else
				{
					if (answerBlock.Text == "Филл Печатает...")
					{
						answerBlock.Text = "Филл Печатает";
					}
					else
					{
						answerBlock.Text += '.';
					}
					// Do something here
				}
			});
			//this.Dispatcher.Invoke(() =>
			//{
			//	answerBlock.Text += "a";
			//});
		}

	

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			//AdminPanel admin = new AdminPanel();
			AdminLogin log = new AdminLogin();
			log.ShowDialog();
		}

		private void DataRow1_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Lang = "ARM";
			Language.Text = "Armenian";
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			Lang = "RU";
			Language.Text = "Russian";
		}
	}
}