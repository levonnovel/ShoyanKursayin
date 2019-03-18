using System;
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
            root.AppendChild(conv);
            doc.Save("logsHistory.xml");
        }
        int count;
        System.Timers.Timer timer = new System.Timers.Timer();

        private void TextBox1_PrevKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {

                if (questionBox.Text.ToLower().Contains("пока") || questionBox.Text.ToLower().Contains("проща") || questionBox.Text.ToLower().Contains("до свидания"))
                {
                    MainWindow context = this;
                    questionBox.Text = "";
                    timer = new System.Timers.Timer();
                    count = 10;
                    ans = "Прощай";
                    answerBlock.Text = "Фил печатает сообщение";
                    timer.Interval = 200;
                    timer.Elapsed += new ElapsedEventHandler(Timer_Tick1);
                    timer.Enabled = true;
                    timer.Start();
                 //   inst.Close();

                }
                else if (questionBox.Text.Length > 0)
                {
                    questionBox.IsEnabled = false;
                    inst.AllPrevQuestionList.Add(questionBox.Text);
                    inst.pos = inst.AllPrevQuestionList.Count;
                    timer = new System.Timers.Timer();
                    questio = questionBox.Text;
                    ans = inst.GetAnswer(questionBox.Text);
                    count = Convert.ToInt32(ans.Length / 4);
                    if (count > 30)
                    {
                        count = 20;
                    }
                    answerBlock.Text = "Фил печатает сообщение";
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
                    answ.InnerText = answerBlock.Text;
                    rep.AppendChild(quest);
                    rep.AppendChild(answ);
                    XmlNodeList convs = doc.GetElementsByTagName("Conversation");
                    XmlNode last = convs.Item(convs.Count - 1);
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
                    hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, new string(' ', 16) + "Фил:  ");
                    hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, "\n");
                    //answerBlock2.Text = hystoryTextBlock.Text.Insert(0, questionBox.Text);
                    hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, "Пользователь:  " + questio);
                    hystoryTextBlock.Text = hystoryTextBlock.Text.Insert(0, "\n");
                    answerBlock.Text = ans;
                    questionBox.IsEnabled = true;
                    questionBox.Focus();
                    timer.Stop();
                }
                else
                {
                    if (answerBlock.Text == "Фил печатает сообщение...")
                    {
                        answerBlock.Text = "Фил печатает сообщение";
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
        void Timer_Tick1(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                count--;
                if (count == 0)
                {
                    answerBlock.Text = ans;
                    inst.Close();
                    timer.Stop();
                }
                else
                {
                    if (answerBlock.Text == "Фил печатает сообщение...")
                    {
                        answerBlock.Text = "Фил печатает сообщение";
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
    }
}