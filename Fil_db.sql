USE [master]
GO
/****** Object:  Database [Fill]    Script Date: 28/05/2018 23:14:06 ******/
CREATE DATABASE [Fill]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Fill', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Fill.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Fill_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Fill_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Fill] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Fill].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Fill] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Fill] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Fill] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Fill] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Fill] SET ARITHABORT OFF 
GO
ALTER DATABASE [Fill] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Fill] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Fill] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Fill] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Fill] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Fill] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Fill] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Fill] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Fill] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Fill] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Fill] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Fill] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Fill] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Fill] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Fill] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Fill] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Fill] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Fill] SET RECOVERY FULL 
GO
ALTER DATABASE [Fill] SET  MULTI_USER 
GO
ALTER DATABASE [Fill] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Fill] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Fill] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Fill] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Fill] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Fill]
GO
/****** Object:  FullTextCatalog [Questions]    Script Date: 28/05/2018 23:14:06 ******/
CREATE FULLTEXT CATALOG [Questions]WITH ACCENT_SENSITIVITY = OFF

GO
/****** Object:  Table [dbo].[Answers]    Script Date: 28/05/2018 23:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[Answer_ID] [int] IDENTITY(1,1) NOT NULL,
	[AnswerText] [nvarchar](1000) NOT NULL,
	[AlterAnswer1] [nvarchar](1000) NULL,
	[AlterAnswer2] [nvarchar](1000) NULL,
	[FullAnswer] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
(
	[Answer_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Questions]    Script Date: 28/05/2018 23:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[Question_ID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionText] [nvarchar](1000) NOT NULL,
	[Answer_ID] [int] NOT NULL,
	[Topic_ID] [int] NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[Question_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Topics]    Script Date: 28/05/2018 23:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Topic] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Topics] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WordsPriorities]    Script Date: 28/05/2018 23:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WordsPriorities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Word] [nvarchar](50) NOT NULL,
	[Priority] [tinyint] NOT NULL,
 CONSTRAINT [PK_WordsPriorities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Answers] ON 

INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (1, N'Здравствуйте! Чем я могу помочь?', N'Здравствуйте! Чем я могу быть Вам полезным?', NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (2, N'Хорошо, спасибо! Что Вас интересует о филиале?', N'У меня всё хорошо! Что Вам рассказать о Филиале?', NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (3, N'Мое полное имя - Филиал, но зовите меня Фил. Вы можете спросить меня о Филиале МГУ в Ереване.', N'Я Фил - виртуальный собеседник для помощи абитуриентам. Можете спросить меня о Филиале МГУ в Ереване. ', NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (6, N'В прошлом году проходной балл на экономический факультет был равен 63-м.', NULL, NULL, N'Для поступления на экономический факультет необходимо было набрать не меньше 63-х баллов: 27 по математике и 36 по русскому языку.')
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (7, N'В прошлом году для поступления на факультет ПМИ нужно было набрать не менее 63-х баллов: 27 по математике и 36 по русскому языку.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (8, N'Основа открытия филиала была заложена в 2011-м году. Сейчас в Филиале обучается больше 120 студентов по шести направлениям подготовки.', NULL, NULL, N'24 октября 2011 года была заложена основа открытия филиала МГУ имени М.В. Ломоносова в Армении на встрече президента Республики Армения Сержа Азатовича Саргсяна со студентами МГУ в Москве. В этот день ректор МГУ Виктор Антонович Садовничий вручил Сержу Саргсяну диплом и медаль за выдающийся вклад в укрепление связей между Россией и Арменией. ')
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (10, N'Филиал имеет 6 направлений подготовки по программе бакалавриата: ПМИ, Экономика, Международные Отношения, Лингвистика, Юриспруденция, Журналистика.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (11, N'В данный момент, в филиале магистратуры нет. Следите за новостями на сайте msu.am.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (12, N'Результаты ЕГЭ не учитываются. На каждый факультет абитуриенты сдают два обязательных экзамена.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (13, N'Нас очень легко найти: РА, 0025 г. Ереван, ул. Айгестана 8. Вход со стороны ул. Вардананц. ', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (14, N'В прошлом году прием начался 20 июня и закончился 10 июля. Подробная информация по этому году будет доступна позднее.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (15, N'
Перечень документов: личное заявление установленного образца, копии документов, удостоверяющих личность и гражданство, оригинал или копия документа о среднем (полном) общем образовании, 4 фотографии размером 3х4
', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (16, N'Преподаватели по профильным предметам приезжают из Москвы', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (17, N'Для поступления на экономический факультет абитуриенты сдают 2 экзамена: русский язык (изложение) и математику', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (18, N'Для поступления на факультет ПМИ абитуриенты сдают 2 экзамена: русский язык (изложение) и математику', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (19, N'Для поступления на журфак абитуриенты сдают 2 экзамена: русский язык (изложение) и литературу', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (20, N'Для поступления на факультет МО абитуриенты сдают 2 экзамена: русский язык (изложение) и историю России', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (21, N'Для поступления на факультет лингвистики абитуриенты сдают 2 экзамена: русский язык (изложение) и английский язык', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (22, N'Для поступления на юрфак абитуриенты сдают 2 экзамена: русский язык (изложение) и историю России.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (23, N'В прошлом году для поступления журналистам хватило 68 баллов из 200 возможных.', NULL, NULL, N'Из 68-и баллов, необходимых для поступления на факультет журналистики, 36 необходимо было набрать на эказмене по русскому языку, а 32 - на экзамене по литературе.')
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (24, N'В прошлом году для поступления на факультет Международных Отношений необходимо было набрать не менее 68 баллов.', NULL, NULL, N'Для поступления на факультет Международных Отношений необходимо было набрать 36 баллов по русскому языку и 32 балла по истории.')
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (25, N'В прошлом году вступительный балл на факультет Юриспруденции был равен 78.', NULL, NULL, N'В прошлом году конкурс на факультет Юриспруденции был одним из самых больших. Для поступления необходимо было набрать по крайней мере 36 баллов по русскому языку и 42 по обществознанию. ')
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (26, N'Для поступления на факультет Лингвистики в прошлом году необходимо было набрать не менее 58 баллов по двум языкам: 36 по русскому и 22 по английскому. ', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (27, N'В прошлом году на программах бакалавра было 15 бюджетных мест.', NULL, NULL, N'В прошлом году 15 бюджетных мест распределились следующим образом: ПМИ - 6, журналистика, лингвистика, экономика - по 3. На направлениях подготовки "Международные Отношения" и "Юриспруденция" обучение только платное.')
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (28, N'Стоимость обучения варьируется: математики и экономисты платят 1,5 миллиона в год, журналисты - 1,6 миллионов, а юристы и международники - 1,8 миллионов.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (1029, N'Я могу предоставить вам информацию об истории филиала, приемной кампании, проходных баллах и экзаменах на разные факультеты, а также о многом другом. Что именно Вас интересует?', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (1030, N'На данный момент в филиале учится 120 студентов.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (1031, N'Экзамены несложные. Материалы для подготовки, а также варианты прошлых лет выложены на сайте msu.am.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (1032, N'Учиться несложно. Главное, заниматься и приходить на пары.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (1033, N'Выпускники Филиала получают диплом Московского государственного университета имени М.В.Ломоносова.', NULL, NULL, NULL)
INSERT [dbo].[Answers] ([Answer_ID], [AnswerText], [AlterAnswer1], [AlterAnswer2], [FullAnswer]) VALUES (2033, N'Каждый экзамен длится ровно 4 часа.', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Answers] OFF
SET IDENTITY_INSERT [dbo].[Questions] ON 

INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1, N'Привет здравствуй', 1, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (2, N'как твои дела тебя', 2, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (3, N'как тебя зовут звать имя', 3, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1007, N'вступительн проходн балл набра поступ экономи', 6, 1)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1011, N'вступительн проходн балл набра поступ математи информатик ПМИ', 7, 1)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1012, N'истори филиал', 8, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1017, N'направлени подготовки список факультет есть бакалавр', 10, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1018, N'прием поступи магистр программ', 11, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1020, N'учитыв балл ЕГЭ результат', 12, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1024, N'адрес филиал где находится', 13, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1025, N'когда начинается начало открывает прием документ', 14, 3)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1026, N'перечень документ представ прием', 15, 3)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1027, N'откуда приезжа кто предпода', 16, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1033, N'экзамен вступительн испытан поступлен экономи', 17, 2)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1037, N'экзамен вступительн испытан поступлен ПМИ математи информатик', 18, 2)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1040, N'экзамен вступительн испытан поступлен журфак журналист', 19, 2)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1042, N'экзамен вступительн испытан поступлен международн отношени', 20, 2)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1044, N'экзамен вступительн испытан поступлен лингвист', 21, 2)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1049, N'экзамен вступительн испытан поступлен юриспруденци юрфак юрист юридическ', 22, 2)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1051, N'вступительн проходн балл набра поступ журфак журналист', 23, 1)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1053, N'вступительн проходн балл набра поступ международн отношени', 24, 1)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1055, N'вступительн проходн балл набра поступ юриспруденци юрфак юрист юридически', 25, 1)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1057, N'вступительн балл проходн набра поступ лингвист', 26, 1)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1059, N'бюджет госзаказ мест бесплатн', 27, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (1060, N'плат сколько стоит обучен стоимост', 28, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (3067, N'что умее може', 1029, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (3069, N'сколько человек людей учится обучается студент филиал', 1030, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (3071, N'экзамен вступительн испытан сложн легк', 1031, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (3072, N'сложн легко учиться', 1032, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (3074, N'диплом выпуск', 1033, NULL)
INSERT [dbo].[Questions] ([Question_ID], [QuestionText], [Answer_ID], [Topic_ID]) VALUES (4073, N'экзамен сколько длит продолж', 2033, NULL)
SET IDENTITY_INSERT [dbo].[Questions] OFF
SET IDENTITY_INSERT [dbo].[Topics] ON 

INSERT [dbo].[Topics] ([ID], [Topic]) VALUES (1, N'вступительные баллы')
INSERT [dbo].[Topics] ([ID], [Topic]) VALUES (2, N'экзамены')
INSERT [dbo].[Topics] ([ID], [Topic]) VALUES (3, N'приемная кампания')
SET IDENTITY_INSERT [dbo].[Topics] OFF
SET IDENTITY_INSERT [dbo].[WordsPriorities] ON 

INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (2, N'кто', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (3, N'когда', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (5, N'что', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (6, N'сколько', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (7, N'каком', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (10, N'как', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (11, N'ты', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (13, N'твое', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (20, N'тебя', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (22, N'какие', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (23, N'есть', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (24, N'какой', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (25, N'филиал', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (27, N'направлени', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (28, N'бакалавр', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (29, N'магистр', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (30, N'привет', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (31, N'здравствуй', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (32, N'дела', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (33, N'твои', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (35, N'зовут', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (36, N'звать', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (37, N'лет', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (38, N'вступительн', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (39, N'балл', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (40, N'набра', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (41, N'поступ', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (42, N'эконом', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (43, N'ПМИ', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (44, N'математик', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (45, N'информатик', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (46, N'журналист', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (47, N'факультет', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (49, N'международн', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (50, N'отношени', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (52, N'юриспруденци', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (53, N'юридическ', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (54, N'юрфак', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (55, N'лигвист', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (56, N'истори', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (57, N'год', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (58, N'основ', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (59, N'заложи', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (60, N'подготовк', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (61, N'список', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (63, N'прием', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (64, N'документ', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (65, N'открывает', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (66, N'начинает', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (67, N'ЕГЭ', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (68, N'результат', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (69, N'начало', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (70, N'перечень', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (71, N'представ', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (72, N'учитыв', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (80, N'адрес', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (81, N'находится', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (82, N'где', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (83, N'откуда', 2)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (84, N'приезжа', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (85, N'препода', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (86, N'экзамен', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (88, N'испытан', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (89, N'поступлен', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (90, N'журфак', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (91, N'юрист', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (93, N'бюджет', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (94, N'госзаказ', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (95, N'мест', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (96, N'бесплатн', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (98, N'нет', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (99, N'плата', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (100, N'стоит', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (101, N'обучен', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (102, N'какова', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (103, N'стоимост', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (104, N'лингвист', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (111, N'количеств', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (112, N'человек', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (114, N'людей', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (115, N'учиться', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (116, N'обучается', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (117, N'студент', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (118, N'сложн', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (119, N'легк', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (120, N'диплом', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (121, N'выпуск', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (1120, N'длит', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (1121, N'продолж', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (1122, N'длят', 3)
INSERT [dbo].[WordsPriorities] ([ID], [Word], [Priority]) VALUES (1123, N'долго', 3)
SET IDENTITY_INSERT [dbo].[WordsPriorities] OFF
/****** Object:  Index [IX_Questions]    Script Date: 28/05/2018 23:14:06 ******/
ALTER TABLE [dbo].[Questions] ADD  CONSTRAINT [IX_Questions] UNIQUE NONCLUSTERED 
(
	[Question_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_WordsPriorities]    Script Date: 28/05/2018 23:14:06 ******/
ALTER TABLE [dbo].[WordsPriorities] ADD  CONSTRAINT [IX_WordsPriorities] UNIQUE NONCLUSTERED 
(
	[Word] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Answers] FOREIGN KEY([Answer_ID])
REFERENCES [dbo].[Answers] ([Answer_ID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Answers]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Topics] FOREIGN KEY([Topic_ID])
REFERENCES [dbo].[Topics] ([ID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Topics]
GO
USE [master]
GO
ALTER DATABASE [Fill] SET  READ_WRITE 
GO
