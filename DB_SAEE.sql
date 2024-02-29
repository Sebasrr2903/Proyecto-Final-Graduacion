/****** Object:  Database [SAEE]    Script Date: 27/2/2024 17:31:36 ******/
CREATE DATABASE SAEE
USE SAEE

/****** Object:  Table [dbo].[ActionReport]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[ActionReport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NOT NULL,
	[userId] [int] NOT NULL,
	[actionDescription] [varchar](500) NOT NULL,
	[origin] [varchar](50) NOT NULL,
 CONSTRAINT [PK_actionReport] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssignmentGrading]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[AssignmentGrading](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[taskId] [int] NOT NULL,
	[studentId] [int] NOT NULL,
	[score] [float] NOT NULL,
	[performanceDescription] [varchar](300) NOT NULL,
 CONSTRAINT [PK_AssignmentGrading] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContentPerWeek]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[ContentPerWeek](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[weekId] [int] NOT NULL,
	[header] [varchar](100) NOT NULL,
	[content] [varchar](500) NOT NULL,
 CONSTRAINT [PK_ContentPerWeek] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseAvailable]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[CourseAvailable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[teacherId] [int] NOT NULL,
	[courseId] [int] NOT NULL,
	[scheduleId] [int] NOT NULL,
	[enrolledStudents] [int] NOT NULL,
	[durationInWeeks] [int] NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_CourseAvailable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseReport]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[CourseReport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[courseId] [int] NOT NULL,
	[studentId] [int] NOT NULL,
	[proceedingId] [int] NOT NULL,
	[finalScore] [float] NOT NULL,
	[performanceDescription] [varchar](300) NOT NULL,
 CONSTRAINT [PK_CourseReport] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[Courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](250) NOT NULL,
	[availableQuota] [int] NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseTasks]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[CourseTasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](250) NOT NULL,
	[File] [varbinary](max) NULL,
	[deadline] [datetime] NOT NULL,
	[weekId] [int] NOT NULL,
 CONSTRAINT [PK_CourseTasks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EnrolledCourses]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[EnrolledCourses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[courseId] [int] NOT NULL,
	[studentId] [int] NOT NULL,
 CONSTRAINT [PK_EnrolledCourses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorReport]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[ErrorReport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NOT NULL,
	[userId] [int] NOT NULL,
	[errorDescription] [varchar](500) NOT NULL,
	[origin] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ErrorReport] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proceedings]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[Proceedings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentId] [int] NOT NULL,
	[average] [float] NOT NULL,
 CONSTRAINT [PK_Proceedings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[Schedule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[day] [varchar](10) NOT NULL,
	[startTime] [varchar](10) NOT NULL,
	[endTime] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialties]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[Specialties](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Specialties] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherData]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[TeacherData](
	[teacherId] [int] NOT NULL,
	[specialty] [int] NOT NULL,
	[experienceYears] [int] NOT NULL,
 CONSTRAINT [PK_TeacherData] PRIMARY KEY CLUSTERED 
(
	[teacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](60) NOT NULL,
	[lastname] [varchar](80) NOT NULL,
	[birthdate] [datetime] NOT NULL,
	[email] [varchar](100) NOT NULL,
	[phoneNumber] [varchar](30) NOT NULL,
	[password] [varchar](300) NOT NULL,
	[active] [bit] NOT NULL,
	[profilePicture] [image] NULL,
	[userType] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersTypes]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[UsersTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UsersTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Weeks]    Script Date: 27/2/2024 17:31:36 ******/
CREATE TABLE [dbo].[Weeks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[weekNum] [int] NOT NULL,
	[courseAvailableId] [int] NOT NULL,
 CONSTRAINT [PK_Weeks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



SET IDENTITY_INSERT [dbo].[ActionReport] ON 
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (19, CAST(N'2024-02-08T15:43:24.673' AS DateTime), 1, N'RegisterUserDone', N'RegisterUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (20, CAST(N'2024-02-08T15:46:56.090' AS DateTime), 1, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (21, CAST(N'2024-02-26T17:37:54.207' AS DateTime), 1, N'ChangeStatusUserDone', N'ChangeStatusUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (22, CAST(N'2024-02-26T17:37:56.233' AS DateTime), 1, N'ChangeStatusUserDone', N'ChangeStatusUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (23, CAST(N'2024-02-27T15:48:50.027' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (24, CAST(N'2024-02-27T16:21:20.877' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (25, CAST(N'2024-02-27T16:23:39.460' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (26, CAST(N'2024-02-27T16:24:01.553' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (27, CAST(N'2024-02-27T16:26:23.130' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (28, CAST(N'2024-02-27T16:27:59.447' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (29, CAST(N'2024-02-27T16:28:09.773' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (32, CAST(N'2024-02-27T17:21:33.547' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (34, CAST(N'2024-02-27T17:23:15.510' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (36, CAST(N'2024-02-27T17:25:10.003' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
INSERT [dbo].[ActionReport] ([id], [date], [userId], [actionDescription], [origin]) VALUES (38, CAST(N'2024-02-27T17:28:28.047' AS DateTime), 6, N'UpdateUserDone', N'UpdateUser')
GO
SET IDENTITY_INSERT [dbo].[ActionReport] OFF
GO
SET IDENTITY_INSERT [dbo].[ContentPerWeek] ON 
GO
INSERT [dbo].[ContentPerWeek] ([id], [weekId], [header], [content]) VALUES (1, 16, N'Introduccion', N'Bienvenido al curso')
GO
INSERT [dbo].[ContentPerWeek] ([id], [weekId], [header], [content]) VALUES (2, 17, N'Semana introductoria', N'En esta semana se abarcaran las bases del español')
GO
INSERT [dbo].[ContentPerWeek] ([id], [weekId], [header], [content]) VALUES (3, 19, N'Verbos y oraciones', N'Se conoceran los verbos y como utilizarlos')
GO
INSERT [dbo].[ContentPerWeek] ([id], [weekId], [header], [content]) VALUES (4, 20, N'Conversacion', N'Se practicaran conversaciones basicas en español')
GO
INSERT [dbo].[ContentPerWeek] ([id], [weekId], [header], [content]) VALUES (7, 13, N'Introduccion', N'Bienvenido al curso de español medio')
GO
INSERT [dbo].[ContentPerWeek] ([id], [weekId], [header], [content]) VALUES (8, 14, N'Semana evaluativa', N'En esta primer semana se realizaran evaluaciones para conocer el nivel de cada estudiante')
GO
INSERT [dbo].[ContentPerWeek] ([id], [weekId], [header], [content]) VALUES (9, 15, N'Inicio de lecciones', N'En esta semana se abarcaran los temas de español conversacional')
GO
SET IDENTITY_INSERT [dbo].[ContentPerWeek] OFF
GO
SET IDENTITY_INSERT [dbo].[CourseAvailable] ON 
GO
INSERT [dbo].[CourseAvailable] ([id], [teacherId], [courseId], [scheduleId], [enrolledStudents], [durationInWeeks], [active]) VALUES (1, 4, 4, 5, 2, 3, 1)
GO
INSERT [dbo].[CourseAvailable] ([id], [teacherId], [courseId], [scheduleId], [enrolledStudents], [durationInWeeks], [active]) VALUES (2, 5, 6, 2, 1, 2, 1)
GO
INSERT [dbo].[CourseAvailable] ([id], [teacherId], [courseId], [scheduleId], [enrolledStudents], [durationInWeeks], [active]) VALUES (6, 4, 6, 5, 0, 5, 1)
GO
SET IDENTITY_INSERT [dbo].[CourseAvailable] OFF
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 
GO
INSERT [dbo].[Courses] ([id], [name], [description], [availableQuota], [active]) VALUES (4, N'Español Básico I', N'Curso enfocado en estudiantes con un manejo nulo del idioma', 5, 1)
GO
INSERT [dbo].[Courses] ([id], [name], [description], [availableQuota], [active]) VALUES (6, N'Español Básico II', N'Curso enfocado en estudiantes con un manejo regular del idioma', 15, 1)
GO
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
SET IDENTITY_INSERT [dbo].[EnrolledCourses] ON 
GO
INSERT [dbo].[EnrolledCourses] ([id], [courseId], [studentId]) VALUES (1, 1, 6)
GO
INSERT [dbo].[EnrolledCourses] ([id], [courseId], [studentId]) VALUES (2, 2, 6)
GO
SET IDENTITY_INSERT [dbo].[EnrolledCourses] OFF
GO
SET IDENTITY_INSERT [dbo].[ErrorReport] ON 
GO
INSERT [dbo].[ErrorReport] ([id], [date], [userId], [errorDescription], [origin]) VALUES (2, CAST(N'2024-02-27T17:16:13.783' AS DateTime), 1, N'An error occurred while updating the entries. See the inner exception for details.', N'DeleteProfilePicture')
GO
INSERT [dbo].[ErrorReport] ([id], [date], [userId], [errorDescription], [origin]) VALUES (3, CAST(N'2024-02-27T17:17:05.177' AS DateTime), 1, N'An error occurred while updating the entries. See the inner exception for details.', N'DeleteProfilePicture')
GO
INSERT [dbo].[ErrorReport] ([id], [date], [userId], [errorDescription], [origin]) VALUES (4, CAST(N'2024-02-27T17:21:39.433' AS DateTime), 1, N'An error occurred while updating the entries. See the inner exception for details.', N'DeleteProfilePicture')
GO
INSERT [dbo].[ErrorReport] ([id], [date], [userId], [errorDescription], [origin]) VALUES (5, CAST(N'2024-02-27T17:23:22.977' AS DateTime), 1, N'An error occurred while updating the entries. See the inner exception for details.', N'DeleteProfilePicture')
GO
INSERT [dbo].[ErrorReport] ([id], [date], [userId], [errorDescription], [origin]) VALUES (6, CAST(N'2024-02-27T17:25:31.933' AS DateTime), 1, N'An error occurred while updating the entries. See the inner exception for details.', N'DeleteProfilePicture')
GO
INSERT [dbo].[ErrorReport] ([id], [date], [userId], [errorDescription], [origin]) VALUES (7, CAST(N'2024-02-27T17:28:35.193' AS DateTime), 1, N'An error occurred while updating the entries. See the inner exception for details.', N'DeleteProfilePicture')
GO
SET IDENTITY_INSERT [dbo].[ErrorReport] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 
GO
INSERT [dbo].[Schedule] ([id], [day], [startTime], [endTime]) VALUES (2, N'Lunes', N'06:00 p.m.', N'09:00 p.m.')
GO
INSERT [dbo].[Schedule] ([id], [day], [startTime], [endTime]) VALUES (4, N'Miércoles', N'08:00 a.m.', N'11:00 a.m.')
GO
INSERT [dbo].[Schedule] ([id], [day], [startTime], [endTime]) VALUES (5, N'Viernes', N'06:00 p.m.', N'09:00 p.m.')
GO
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[Specialties] ON 
GO
INSERT [dbo].[Specialties] ([id], [description]) VALUES (1, N' Bachillerato en Ciencias de la Educación con énfasis en Español')
GO
INSERT [dbo].[Specialties] ([id], [description]) VALUES (2, N'Bachillerato en la Enseñanza del Español')
GO
INSERT [dbo].[Specialties] ([id], [description]) VALUES (3, N'Bachillerato en la Enseñanza del Español para Extranjeros')
GO
INSERT [dbo].[Specialties] ([id], [description]) VALUES (4, N'Bachillerato en Lingüística con especialidad en Español')
GO
INSERT [dbo].[Specialties] ([id], [description]) VALUES (5, N'Magister en Español como Segunda Lengua')
GO
SET IDENTITY_INSERT [dbo].[Specialties] OFF
GO
INSERT [dbo].[TeacherData] ([teacherId], [specialty], [experienceYears]) VALUES (4, 1, 10)
GO
INSERT [dbo].[TeacherData] ([teacherId], [specialty], [experienceYears]) VALUES (5, 2, 15)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([id], [name], [lastname], [birthdate], [email], [phoneNumber], [password], [active], [profilePicture], [userType]) VALUES (1, N'Admin', N'Admin', CAST(N'2002-03-05T00:00:00.000' AS DateTime), N'admin@gmail.com', N'11111111', N'3e6dc62f220c57f4e44e3dd541c175b3a4fd22986bafa16d47ce3d4c2b224ac8', 1, NULL, 1)
GO
INSERT [dbo].[Users] ([id], [name], [lastname], [birthdate], [email], [phoneNumber], [password], [active], [profilePicture], [userType]) VALUES (4, N'Juan', N'Soto', CAST(N'2002-03-05T00:00:00.000' AS DateTime), N'soto@gmail.com', N'33333333', N'3e6dc62f220c57f4e44e3dd541c175b3a4fd22986bafa16d47ce3d4c2b224ac8', 1, NULL, 2)
GO
INSERT [dbo].[Users] ([id], [name], [lastname], [birthdate], [email], [phoneNumber], [password], [active], [profilePicture], [userType]) VALUES (5, N'Marta', N'Saez', CAST(N'2002-03-05T00:00:00.000' AS DateTime), N'saez@gmail.com', N'44444444', N'3e6dc62f220c57f4e44e3dd541c175b3a4fd22986bafa16d47ce3d4c2b224ac8', 1, NULL, 2)
GO
INSERT [dbo].[Users] ([id], [name], [lastname], [birthdate], [email], [phoneNumber], [password], [active], [profilePicture], [userType]) VALUES (6, N'FERNANDO ', N'PEREZ ALPIZAR', CAST(N'2002-03-05T00:00:00.000' AS DateTime), N'fperezalpizar@gmail.com', N'00000000', N'3e6dc62f220c57f4e44e3dd541c175b3a4fd22986bafa16d47ce3d4c2b224ac8', 1, NULL, 3)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UsersTypes] ON 
GO
INSERT [dbo].[UsersTypes] ([id], [description]) VALUES (1, N'Administrador')
GO
INSERT [dbo].[UsersTypes] ([id], [description]) VALUES (2, N'Profesor')
GO
INSERT [dbo].[UsersTypes] ([id], [description]) VALUES (3, N'Estudiante')
GO
SET IDENTITY_INSERT [dbo].[UsersTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Weeks] ON 
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (7, 0, 6)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (8, 1, 6)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (9, 2, 6)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (10, 3, 6)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (11, 4, 6)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (12, 5, 6)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (13, 0, 2)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (14, 1, 2)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (15, 2, 2)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (16, 0, 1)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (17, 1, 1)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (19, 2, 1)
GO
INSERT [dbo].[Weeks] ([id], [weekNum], [courseAvailableId]) VALUES (20, 3, 1)
GO
SET IDENTITY_INSERT [dbo].[Weeks] OFF
GO



/****** Object:  Index [IX_ContentPerWeek]    Script Date: 27/2/2024 17:31:36 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ContentPerWeek] ON [dbo].[ContentPerWeek]
(
	[weekId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users]    Script Date: 27/2/2024 17:31:36 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [IX_Users] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ActionReport]  WITH CHECK ADD  CONSTRAINT [FK_actionReport_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[ActionReport] CHECK CONSTRAINT [FK_actionReport_Users]
GO
ALTER TABLE [dbo].[AssignmentGrading]  WITH CHECK ADD  CONSTRAINT [FK_AssignmentGrading_CourseTasks] FOREIGN KEY([taskId])
REFERENCES [dbo].[CourseTasks] ([id])
GO
ALTER TABLE [dbo].[AssignmentGrading] CHECK CONSTRAINT [FK_AssignmentGrading_CourseTasks]
GO
ALTER TABLE [dbo].[AssignmentGrading]  WITH CHECK ADD  CONSTRAINT [FK_AssignmentGrading_Users] FOREIGN KEY([studentId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[AssignmentGrading] CHECK CONSTRAINT [FK_AssignmentGrading_Users]
GO
ALTER TABLE [dbo].[ContentPerWeek]  WITH CHECK ADD  CONSTRAINT [FK_ContentPerWeek_Weeks] FOREIGN KEY([weekId])
REFERENCES [dbo].[Weeks] ([id])
GO
ALTER TABLE [dbo].[ContentPerWeek] CHECK CONSTRAINT [FK_ContentPerWeek_Weeks]
GO
ALTER TABLE [dbo].[CourseAvailable]  WITH CHECK ADD  CONSTRAINT [FK_CourseAvailable_Courses] FOREIGN KEY([courseId])
REFERENCES [dbo].[Courses] ([id])
GO
ALTER TABLE [dbo].[CourseAvailable] CHECK CONSTRAINT [FK_CourseAvailable_Courses]
GO
ALTER TABLE [dbo].[CourseAvailable]  WITH CHECK ADD  CONSTRAINT [FK_CourseAvailable_Schedule] FOREIGN KEY([scheduleId])
REFERENCES [dbo].[Schedule] ([id])
GO
ALTER TABLE [dbo].[CourseAvailable] CHECK CONSTRAINT [FK_CourseAvailable_Schedule]
GO
ALTER TABLE [dbo].[CourseAvailable]  WITH CHECK ADD  CONSTRAINT [FK_CourseAvailable_TeacherData] FOREIGN KEY([teacherId])
REFERENCES [dbo].[TeacherData] ([teacherId])
GO
ALTER TABLE [dbo].[CourseAvailable] CHECK CONSTRAINT [FK_CourseAvailable_TeacherData]
GO
ALTER TABLE [dbo].[CourseReport]  WITH CHECK ADD  CONSTRAINT [FK_CourseReport_Courses] FOREIGN KEY([courseId])
REFERENCES [dbo].[CourseAvailable] ([id])
GO
ALTER TABLE [dbo].[CourseReport] CHECK CONSTRAINT [FK_CourseReport_Courses]
GO
ALTER TABLE [dbo].[CourseReport]  WITH CHECK ADD  CONSTRAINT [FK_CourseReport_Proceedings] FOREIGN KEY([proceedingId])
REFERENCES [dbo].[Proceedings] ([id])
GO
ALTER TABLE [dbo].[CourseReport] CHECK CONSTRAINT [FK_CourseReport_Proceedings]
GO
ALTER TABLE [dbo].[CourseReport]  WITH CHECK ADD  CONSTRAINT [FK_CourseReport_Users] FOREIGN KEY([studentId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[CourseReport] CHECK CONSTRAINT [FK_CourseReport_Users]
GO
ALTER TABLE [dbo].[CourseTasks]  WITH CHECK ADD  CONSTRAINT [FK_CourseTasks_Courses] FOREIGN KEY([weekId])
REFERENCES [dbo].[Weeks] ([id])
GO
ALTER TABLE [dbo].[CourseTasks] CHECK CONSTRAINT [FK_CourseTasks_Courses]
GO
ALTER TABLE [dbo].[EnrolledCourses]  WITH CHECK ADD  CONSTRAINT [FK_EnrolledCourses_CourseAvailable] FOREIGN KEY([courseId])
REFERENCES [dbo].[CourseAvailable] ([id])
GO
ALTER TABLE [dbo].[EnrolledCourses] CHECK CONSTRAINT [FK_EnrolledCourses_CourseAvailable]
GO
ALTER TABLE [dbo].[EnrolledCourses]  WITH CHECK ADD  CONSTRAINT [FK_EnrolledCourses_Users] FOREIGN KEY([studentId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[EnrolledCourses] CHECK CONSTRAINT [FK_EnrolledCourses_Users]
GO
ALTER TABLE [dbo].[ErrorReport]  WITH CHECK ADD  CONSTRAINT [FK_ErrorReport_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[ErrorReport] CHECK CONSTRAINT [FK_ErrorReport_Users]
GO
ALTER TABLE [dbo].[Proceedings]  WITH CHECK ADD  CONSTRAINT [FK_Proceedings_Users] FOREIGN KEY([studentId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Proceedings] CHECK CONSTRAINT [FK_Proceedings_Users]
GO
ALTER TABLE [dbo].[TeacherData]  WITH CHECK ADD  CONSTRAINT [FK_TeacherData_Specialties] FOREIGN KEY([specialty])
REFERENCES [dbo].[Specialties] ([id])
GO
ALTER TABLE [dbo].[TeacherData] CHECK CONSTRAINT [FK_TeacherData_Specialties]
GO
ALTER TABLE [dbo].[TeacherData]  WITH CHECK ADD  CONSTRAINT [FK_TeacherData_Users] FOREIGN KEY([teacherId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[TeacherData] CHECK CONSTRAINT [FK_TeacherData_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UsersTypes] FOREIGN KEY([userType])
REFERENCES [dbo].[UsersTypes] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UsersTypes]
GO
ALTER TABLE [dbo].[Weeks]  WITH CHECK ADD  CONSTRAINT [FK_Weeks_CourseAvailable] FOREIGN KEY([courseAvailableId])
REFERENCES [dbo].[CourseAvailable] ([id])
GO
ALTER TABLE [dbo].[Weeks] CHECK CONSTRAINT [FK_Weeks_CourseAvailable]
GO



/****** Object:  StoredProcedure [dbo].[SP_Login]    Script Date: 27/2/2024 17:31:36 ******/
CREATE PROCEDURE [dbo].[SP_Login]
	@Email varchar(100),
	@Password varchar(300)
AS
BEGIN
	SELECT id,
		   name,
		   lastname,
		   email,
		   phoneNumber,
		   password,
		   active,
		   profilePicture,
		   userType
	  FROM dbo.Users
	  WHERE email = @Email
	  AND password = @Password
	  AND Active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RegisterUser]    Script Date: 27/2/2024 17:31:36 ******/
CREATE PROCEDURE [dbo].[SP_RegisterUser]
	@Name varchar(60),
    @Lastname varchar(80),
	@Birthdate datetime,
    @Email varchar(100),
    @PhoneNumber varchar(30),
	@Password varchar(300),
	@UserType int,
	@Specialty int, --If they are a teacher
	@ExperienceYears int --If they are a teacher
AS
BEGIN
	DECLARE @userId AS BIGINT;
	DECLARE @registeredEmail bit;

	--Check that the email is not repeated
	IF EXISTS(SELECT * FROM Users U WHERE U.email = @Email)
	SET @registeredEmail = 1
	ELSE
	SET @registeredEmail = 0
	

	IF (@registeredEmail = 0)
	BEGIN
		INSERT INTO dbo.Users (name, lastname, birthdate, email, phoneNumber, password, active, userType)
		VALUES (@Name, @Lastname, @Birthdate, @Email, @PhoneNumber, @Password, 1, @UserType)

		IF (@UserType = 2) --Teacher
		BEGIN
			SET @userId = @@IDENTITY --Last id by identity created (from users table to have the same in teachers table)

			INSERT INTO dbo.TeacherData(teacherId, specialty, experienceYears)
			VALUES (@userId, @Specialty, @ExperienceYears)
		END

		SELECT 'OK' 'Message'
	END	

	ELSE IF (@registeredEmail = 1)
	BEGIN
		SELECT 'Repeated email' 'Message'
	END
END
GO
USE [master]
GO
ALTER DATABASE [SAEE] SET  READ_WRITE 
GO
