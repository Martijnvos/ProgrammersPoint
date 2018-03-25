DROP TABLE Scheldwoord
DROP TABLE Reactie
DROP TABLE ReviewWaardering
DROP TABLE Review
DROP TABLE Post_Categorie
DROP TABLE Post
DROP TABLE Gebruiker_Categorie
DROP TABLE Categorie
DROP TABLE Gebruiker

DROP PROCEDURE PostInsertProcedure
DROP PROCEDURE GebruikerInsertProcedure
DROP PROCEDURE PostGetNieuwePostsProcedure
DROP PROCEDURE ReviewWaarderingInsertOrUpdate

DROP TRIGGER trigger_Post_INSERT
DROP TRIGGER trigger_Post_UPDATE
DROP TRIGGER trigger_Review_INSERT
DROP TRIGGER trigger_Review_UPDATE

CREATE PROCEDURE GebruikerInsertProcedure(
	@Gebruikersnaam NVARCHAR(30),
	@Wachtwoord NVARCHAR(MAX),
	@Emailadres NVARCHAR(450)
) AS
	INSERT INTO Gebruiker (gebruikersnaam, wachtwoord, emailadres) 
	VALUES(@Gebruikersnaam, @Wachtwoord, @Emailadres)
GO

CREATE TABLE Gebruiker (
	gebruikerId	INT	IDENTITY(1, 1)	PRIMARY KEY,
	postcount	INT	DEFAULT 0	NOT NULL,
	gebruikersnaam	NVARCHAR(30)	NOT NULL UNIQUE,
	wachtwoord	NVARCHAR(MAX)	NOT NULL,
	badge		NVARCHAR(MAX)	DEFAULT 'Nieuw',
	badgetekst	NVARCHAR(30)	DEFAULT 'Nieuwe gebruiker',
	karma		INT		DEFAULT 0 NOT NULL,
	emailadres	NVARCHAR(450)	NOT NULL UNIQUE,
	functie		INT		DEFAULT 0 NOT NULL,
	CONSTRAINT EmailConstraint	CHECK (emailadres LIKE '%_@__%.__%')
);

CREATE TABLE Categorie (
	categorie	INT		IDENTITY(1, 1)	PRIMARY KEY,
	naam		NVARCHAR(MAX)	NOT NULL,
	omschrijving	NVARCHAR(MAX)	NOT NULL
);

CREATE TABLE Gebruiker_Categorie (
	gebruikerId			INT 		NOT NULL REFERENCES Gebruiker(gebruikerId),
	categorie			INT 		NOT NULL REFERENCES Categorie(categorie),
	[aantal bekeken]		INT		NOT NULL	DEFAULT 1
);

CREATE TABLE Post (
	postId				INT		IDENTITY(1, 1)	PRIMARY KEY,
	categorie			INT		NOT NULL REFERENCES Categorie(categorie),
	[beschrijving taal]		NVARCHAR(MAX)	NOT NULL,
	[laatst geüpdatet]		DateTime	NOT NULL	DEFAULT GETDATE(),
	postversie			INT		NOT NULL	DEFAULT 1,
	taalversie			NVARCHAR(MAX)	NOT NULL,
	[moeilijkheidsgraad onderwerp]	TINYINT		NOT NULL,
	naam				NVARCHAR(MAX)	NOT NULL,
	[reports postbeschrijving]	INT		NOT NULL 	DEFAULT 0
);

CREATE TABLE Post_Categorie (
	postId				INT		NOT NULL REFERENCES Post(postId),
	categorie			INT		NOT NULL REFERENCES Categorie(categorie)
);

CREATE TABLE Review (
	reviewId		INT		IDENTITY(1, 1)	PRIMARY KEY,
	postId			INT		NOT NULL REFERENCES Post(postId),
	gebruikerId		INT		NOT NULL REFERENCES Gebruiker(gebruikerId),
	reviewtekst		NVARCHAR(MAX)	NOT NULL,
	titel			NVARCHAR(30)	NOT NULL
);

CREATE TABLE ReviewWaardering (
	reviewWaarderingId	INT		IDENTITY(1, 1)	PRIMARY KEY,
	gebruikerId		INT		NOT NULL	REFERENCES Gebruiker(gebruikerId),
	reviewId		INT		NOT NULL	REFERENCES Review(reviewId),		
	upvote			BIT		NOT NULL,
	downvote		BIT		NOT NULL,
	report			BIT		NOT NULL
);

CREATE TABLE Reactie (
	reactieId		INT		IDENTITY(1, 1)	PRIMARY KEY,
	reactieOpReactieId	INT		REFERENCES Reactie(reactieId),
	gebruikerId		INT		NOT NULL REFERENCES Gebruiker(gebruikerId),
	reviewId		INT		NOT NULL REFERENCES Review(reviewId),
	inhoud			NVARCHAR(MAX)	NOT NULL,
	[aantal reports]	INT		NOT NULL	DEFAULT 0,
	datum			DATE		NOT NULL	DEFAULT GETDATE()
);

CREATE TABLE Scheldwoord (
	scheldwoordId		INT		IDENTITY(1, 1)	PRIMARY KEY,
	woord			NVARCHAR(450)	NOT NULL	UNIQUE
);