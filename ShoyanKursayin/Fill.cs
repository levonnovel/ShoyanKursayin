using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShoyanKursayin
{
    class Fill
    {
        #region Fields
        MainWindow context;
        Dictionary<int, string> Rows = new Dictionary<int, string>();
        Dictionary<int, int> RowsPriorities = new Dictionary<int, int>();
        Dictionary<int, string> Topics = new Dictionary<int, string>();
        Dictionary<int, int> RowID_TopicsID = new Dictionary<int, int>();
        Dictionary<string, int> WordPriorities = new Dictionary<string, int>();
        List<string> PrevAnswersList = new List<string>();
        public List<string> PrevQuestionsList = new List<string>();
        public int pos = 0;
        public List<string> AllPrevQuestionList = new List<string>();
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        string StandardQuestion { get; set; } = "Могу я помочь чем-то ещё?";
        string AmbigousQuestion { get; set; } = "Что вас конкретно интересует по теме " + "\"";
        string NoQuestion { get; set; } = "Я не знаю ответ на Ваш вопрос. Пожалуйста, переформулируйте его.";
        string AlsoAnswered { get; set; } = "Мне кажется, я уже отвечал на этот вопрос. Пожалуйста, проверьте историю нашего диалога.";
        string LastQuestion { get; set; }
        string StandardLongQuestion { get; set; } = " Хотите получить более подробную информацию об этом?";
        string FullAnswer { get; set; }
        int maxVal = 0, questionKey = 0, prevQuestionKey = 0;
        bool isLongAnswer = false;
        bool isAmbigousAnwer = true;
        bool isConcreteAnswer = false;
        #endregion Fields
        #region Constructor
        public Fill(MainWindow context)
        {
            this.context = context;
            GetData();
        }
        #endregion Constructor
        #region Methods
        // Method for closing window
        public void Close()
        {
            Thread t = new Thread(() =>
            {
                Thread.Sleep(2000);
                context.Dispatcher.Invoke(() =>
                {
                    context.Close();
                });
            });
            t.Start();
        }
        // Clear old values for making new checks 
        void Clear()
        {
            foreach (KeyValuePair<int, int> elem in RowsPriorities.ToList())
            {
                RowsPriorities[elem.Key] = 0;
            }
            maxVal = 0;
            questionKey = 0;
            //prevQuestionKey = 0;
            isConcreteAnswer = false;
            isAmbigousAnwer = false;
        }
        // Method that returns the final answer of our question
        public string GetAnswer(string currentQuestion)
        {
            if (isLongAnswer)
            {
                isLongAnswer = false;
                if (((currentQuestion.ToLower().Contains("да") || currentQuestion.ToLower().Contains("хочу") || currentQuestion.ToLower().Contains("давай")) || (currentQuestion.ToLower().Contains("конечно"))) && ((!currentQuestion.ToLower().Contains(" не ")) && (!currentQuestion.ToLower().Contains(" нет"))))
                {
                    return FullAnswer;
                }

                //if (currentQuestion.ToLower() == "да" || currentQuestion.ToLower() == "хочу" || currentQuestion.ToLower() == "давай")
                //{
                //    return FullAnswer;
                //}
                else
                {
                    LastQuestion = StandardQuestion;
                    return StandardQuestion;
                }
            }
            if (LastQuestion == StandardQuestion)
            {
                string answer = String.Empty;
                if (((currentQuestion.ToLower().Contains("хочу")) || (currentQuestion.ToLower().Contains("да")) || (currentQuestion.ToLower().Contains("конечно"))) && (!currentQuestion.ToLower().Contains(" не ")) && (!currentQuestion.ToLower().Contains(" нет"))) {
                    LastQuestion = null;
                    answer = "Замечательно! Чем ещё я могу Вам помочь?";
                }
                else if ((currentQuestion.ToLower().Contains("не ")) || (currentQuestion.ToLower().Contains("нет")))
                {
                    LastQuestion = null;
                    answer = "До свидания!";
                    Close();
                }
                //switch (currentQuestion.ToLower())
                //{
                //    case "да":
                //    case "хочу":
                //        LastQuestion = null;
                //        answer = "Чем я могу помочь?";
                //        break;
                //    case "нет":
                //    case "не хочу":
                //        LastQuestion = null;

                //        answer = "До свидания!";
                //        Close();
                //        break;
                //}
                if (answer != string.Empty)
                {
                    return answer;
                }
            }
            if (currentQuestion.ToLower().Contains("спасибо") || currentQuestion.ToLower().Contains("круто") || currentQuestion.ToLower().Contains("здорово"))
            {
                LastQuestion = StandardQuestion;
                return LastQuestion;
            }
            else
            {
                LastQuestion = null;
            }
            string temp = null;
            RemoveRedundantWords(ref currentQuestion);
            if (String.IsNullOrEmpty(currentQuestion))
            {
                return NoQuestion;
            }
            FindQuestion(currentQuestion);
            GetAnswerType(currentQuestion);
            if (isConcreteAnswer)
            {
                string ans = Answer(currentQuestion);
                PrevQuestionsList.Add(currentQuestion);
                Clear();
                if (!PrevAnswersList.Contains(FindAnswerID(ans)))
                {
                    PrevAnswersList.Add(FindAnswerID(ans));
                    if (isLongAnswer)
                    {
                        return ans + StandardLongQuestion;
                    }
                    return ans;
                }
                else
                {
                    return AlsoAnswered;
                }
            }
            else
            {
                for (int i = (PrevQuestionsList.Count - 1); i >= 0; i--)
                {
                    List<string> qq = PrevQuestionsList[i].Split(' ').ToList<string>();
                    foreach (string el in Find(qq))
                    {
                        if (isAmbigousAnwer)
                        {
                            Clear();
                            prevQuestionKey = i;
                            temp = el + ' ' + currentQuestion;
                            FindQuestion(temp);
                            GetAnswerType(temp);
                        }
                        else
                        {
                            goto Home;
                        }
                    }


                }
            }
            foreach (string el in Find(PrevQuestionsList))
            {
                if (isAmbigousAnwer)
                {
                    Clear();
                    temp = el + ' ' + currentQuestion;
                    FindQuestion(temp);
                    GetAnswerType(temp);
                }
                else
                {
                    goto Home;
                }
            }
            Home:
            PrevQuestionsList.Add(currentQuestion);
            string topicName = GetTopic(questionKey);
            if (isConcreteAnswer)
            {
                if (prevQuestionKey != 0)
                {
                    PrevQuestionsList.RemoveRange(prevQuestionKey + 1, PrevQuestionsList.Count - prevQuestionKey - 1);
                    PrevQuestionsList.Add(currentQuestion);
                }
                string answer = Answer((temp));
                Clear();
                if (!PrevAnswersList.Contains(FindAnswerID(answer)))
                {
                    PrevAnswersList.Add(FindAnswerID(answer));
                    if (isLongAnswer)
                    {
                        return answer + StandardLongQuestion;
                    }
                    return answer;
                    //return answer;
                }
                else
                {
                    return AlsoAnswered;
                }
            }
            else
            {
                Clear();
                if (String.IsNullOrEmpty(topicName))
                {
                    return AmbigousQuestion + currentQuestion + '\"';
                }
                return AmbigousQuestion + topicName + '\"';
            }

        }
        string FindAnswerID(string ans)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                StringBuilder st = new StringBuilder("Select Answers.Answer_ID FROM Answers WHERE AnswerText = @answer OR AlterAnswer1 = @answer OR AlterAnswer2 = @answer");
                SqlCommand cmd = new SqlCommand(st.ToString(), conn);
                SqlParameter paramID = new SqlParameter("@answer", ans);
                cmd.Parameters.Add(paramID);
                SqlDataReader dr2 = cmd.ExecuteReader();
                Random r = new Random();
                while (dr2.Read())
                {
                    return dr2["Answer_ID"].ToString();
                }
            }
            return null;
        }
        // Removes redundant words
        void RemoveRedundantWords(ref string question)
        {
            question = Regex.Replace(question, "[,.?!]", string.Empty);
            List<string> words = question.ToLower().Split(' ').ToList();
            List<string> oddWords = new List<string>();
            //string[] words = question.ToLower().Split(' ').ToList();
            foreach (string elem in words)
            {
                foreach (KeyValuePair<string, int> kv in WordPriorities)
                {
                    if (elem.Contains(kv.Key))
                    {
                        goto next;
                    }
                }
                //List<string> l = words.ToList<string>();
                oddWords.Add(elem);
                //l.RemoveAt(Array.IndexOf(words, elem));
                next:;
            }
            foreach (string elem in oddWords)
            {
                words.Remove(elem);
            }
            question = String.Join(" ", words);
        }
        // Method that returns the answer of the question
        string Answer(string s)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                List<string> answers = new List<string>();
                conn.Open();
                StringBuilder st2 = new StringBuilder("Select Answers.AnswerText, Answers.AlterAnswer1, Answers.AlterAnswer2, Answers.FullAnswer From Questions Left Join Answers ON Answers.Answer_ID = Questions.Answer_ID WHERE Questions.Question_ID = ");
                st2.Append(questionKey);
                SqlCommand cmd2 = new SqlCommand(st2.ToString(), conn);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                Random r = new Random();
                while (dr2.Read())
                {
                    answers.Add(dr2["AnswerText"].ToString());
                    if (!String.IsNullOrEmpty(dr2["FullAnswer"].ToString()))
                    {
                        isLongAnswer = true;
                        FullAnswer = dr2["FullAnswer"].ToString();
                    }
                    if (!String.IsNullOrEmpty(dr2["AlterAnswer1"].ToString()))
                    {
                        answers.Add(dr2["AlterAnswer1"].ToString());
                    }
                    if (!String.IsNullOrEmpty(dr2["AlterAnswer2"].ToString()))
                    {
                        answers.Add(dr2["AlterAnswer2"].ToString());
                    }
                    Clear();

                    return answers[r.Next(0, answers.Count)];

                }
            }
            return null;
        }
        // Method that returns the type of the answer (concrete of ambigious)
        void GetAnswerType(string s)
        {
            foreach (KeyValuePair<int, int> elem1 in RowsPriorities)
            {
                foreach (KeyValuePair<int, int> elem2 in RowsPriorities)
                {
                    if (elem1.Value == elem2.Value && elem1.Key != elem2.Key && elem1.Value == maxVal && maxVal != 0)
                    {
                        LastQuestion += " " + s;
                        isConcreteAnswer = false;
                        isAmbigousAnwer = true;
                        return;
                    }
                }
            }

            if (maxVal == 0)
            {
                isConcreteAnswer = false;
                isAmbigousAnwer = false;
            }
            else
            {
                isConcreteAnswer = true;
                isAmbigousAnwer = false;
            }
        }
        // Method that checks whether our answer has a topic or no
        string GetTopic(int questionKey)
        {
            if (RowID_TopicsID.Keys.Contains(questionKey) && Topics.Keys.Contains(RowID_TopicsID[questionKey]))
            {
                return Topics[RowID_TopicsID[questionKey]];
            }
            return null;
        }
        // Method that is called in constructor
        // It collects all data from our database
        void GetData()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                string query1 = "SELECT Question_ID, QuestionText FROM Questions";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                string s;
                while (dr1.Read())
                {
                    s = (string)dr1["QuestionText"];
                    Rows.Add((int)dr1["Question_ID"], s.ToLower());
                }
                dr1.Close();
                string query2 = "SELECT Word, Priority FROM WordsPriorities";
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    s = (string)dr2["Word"];
                    WordPriorities.Add(s.ToLower(), Convert.ToInt32(dr2["Priority"]));
                }
                dr2.Close();
                foreach (KeyValuePair<int, string> keyVal in Rows)
                {
                    string[] QTextArr = keyVal.Value.Split(' ');

                    RowsPriorities.Add(keyVal.Key, 0);
                }

                string query3 = "SELECT * FROM Topics";
                SqlCommand cmd3 = new SqlCommand(query3, conn);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    Topics.Add(Convert.ToInt32(dr3["ID"]), Convert.ToString(dr3["Topic"]));
                }
                dr3.Close();

                string query4 = "SELECT Question_ID, Topic_ID FROM Questions";
                SqlCommand cmd4 = new SqlCommand(query4, conn);
                SqlDataReader dr4 = cmd4.ExecuteReader();
                while (dr4.Read())
                {
                    if (dr4["Topic_ID"] != DBNull.Value)
                    {
                        RowID_TopicsID.Add(Convert.ToInt32(dr4["Question_ID"]), Convert.ToInt32(dr4["Topic_ID"]));
                    }
                }
                dr3.Close();
            }
        }
        // Method that returns all possible questions from list
        static IEnumerable<string> Find(List<string> str)
        {
            for (int i = str.Count - 1; i >= 0; i--)
            {
                for (var j = 1; j < str.Count - i + 1; j++)
                {
                    string temp = "";
                    foreach (string k in str.GetRange(i, j))
                    {
                        temp += k + " ";
                    }
                    yield return temp;
                }
            }
        }
        // Method that accepts the question and trying to understand which question from db is thet
        void FindQuestion(string question)
        {
            question = Regex.Replace(question, "[,.?!]", string.Empty);

            string[] currentQuestionArr = question.ToLower().Split(' ');
            Dictionary<int, List<string>> usedWords = new Dictionary<int, List<string>>();

            foreach (string elem in currentQuestionArr)
            {
                if (elem.Length > 1)
                {
                    foreach (KeyValuePair<int, string> keyVal in Rows)
                    {
                        if (!usedWords.ContainsKey(keyVal.Key))
                        {
                            usedWords.Add(keyVal.Key, new List<string>());
                        }
                        string[] QTextArr = keyVal.Value.Split(' ');
                        foreach (string QTextWord in QTextArr)
                        {
                            if (elem.Contains(QTextWord) && !usedWords[keyVal.Key].Contains(QTextWord))
                            {
                                usedWords[keyVal.Key].Add(elem);
                                foreach (KeyValuePair<string, int> wordPrior in WordPriorities)
                                {
                                    if (elem.Contains(wordPrior.Key))
                                    {
                                        RowsPriorities[keyVal.Key] += wordPrior.Value;
                                        break;
                                    }
                                }
                            }

                        }
                        //usedWords.Clear();
                    }
                }
            }
            foreach (KeyValuePair<int, int> elem in RowsPriorities)
            {
                if (elem.Value > maxVal)
                {
                    maxVal = elem.Value;
                    questionKey = elem.Key;
                }
            }
        }
        #endregion Methods
    }
}