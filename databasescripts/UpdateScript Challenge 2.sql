-- Triggers voor Post
CREATE TRIGGER trigger_Post_INSERT
ON Post INSTEAD OF INSERT
AS
	DECLARE @PostId INT
	DECLARE @Categorie INT
	DECLARE @BeschrijvingTaal NVARCHAR(MAX)
	DECLARE @LaatstGeüpdatet DATETIME
	DECLARE @Postversie INT
	DECLARE @Taalversie NVARCHAR(MAX)
	DECLARE @MoeilijkheidsgraadOnderwerp TINYINT
	DECLARE @Naam NVARCHAR(MAX)
	DECLARE @ReportsPostbeschrijving INT

	SELECT @PostId = postId, @Categorie = categorie, @BeschrijvingTaal = [beschrijving taal], 
	@LaatstGeüpdatet = [laatst geüpdatet], @Postversie = postversie, @Taalversie = taalversie,
	@MoeilijkheidsgraadOnderwerp = [moeilijkheidsgraad onderwerp], @Naam = naam, @ReportsPostbeschrijving = [reports postbeschrijving] FROM inserted

	DECLARE @Scheldwoorden TABLE(scheldwoordId INT, woord NVARCHAR(MAX))
	INSERT INTO @Scheldwoorden (scheldwoordId, woord) SELECT * FROM Scheldwoord

	UPDATE @Scheldwoorden
	SET @BeschrijvingTaal = REPLACE(@BeschrijvingTaal, woord, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(woord, 'A', '*'), 'E', '*'), 'O', '*'), 'U', '*'), 'I', '*'))
	INSERT INTO Post (categorie, [beschrijving taal], [laatst geüpdatet], postversie,
	taalversie, [moeilijkheidsgraad onderwerp], naam, [reports postbeschrijving]) 
	VALUES(@Categorie, @BeschrijvingTaal, @LaatstGeüpdatet, @Postversie, @Taalversie, @MoeilijkheidsgraadOnderwerp, @Naam, @ReportsPostbeschrijving)
GO

CREATE TRIGGER trigger_Post_UPDATE
ON Post INSTEAD OF UPDATE
AS
	DECLARE @PostId INT
	DECLARE @Categorie INT
	DECLARE @BeschrijvingTaal NVARCHAR(MAX)
	DECLARE @LaatstGeüpdatet DATETIME
	DECLARE @Postversie INT
	DECLARE @Taalversie NVARCHAR(MAX)
	DECLARE @MoeilijkheidsgraadOnderwerp TINYINT
	DECLARE @Naam NVARCHAR(MAX)
	DECLARE @ReportsPostbeschrijving INT

	SELECT @PostId = postId, @Categorie = categorie, @BeschrijvingTaal = [beschrijving taal], 
	@LaatstGeüpdatet = [laatst geüpdatet], @Postversie = postversie, @Taalversie = taalversie,
	@MoeilijkheidsgraadOnderwerp = [moeilijkheidsgraad onderwerp], @Naam = naam, @ReportsPostbeschrijving = [reports postbeschrijving] FROM inserted

	DECLARE @Scheldwoorden TABLE(scheldwoordId INT, woord NVARCHAR(MAX))
	INSERT INTO @Scheldwoorden (scheldwoordId, woord) SELECT * FROM Scheldwoord

	UPDATE @Scheldwoorden
	SET @BeschrijvingTaal = REPLACE(@BeschrijvingTaal, woord, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(woord, 'A', '*'), 'E', '*'), 'O', '*'), 'U', '*'), 'I', '*'))
	UPDATE Post SET categorie = @Categorie, [beschrijving taal] = @BeschrijvingTaal, [laatst geüpdatet] = @LaatstGeüpdatet,
	postversie = @Postversie, taalversie = @Taalversie, [moeilijkheidsgraad onderwerp] = @MoeilijkheidsgraadOnderwerp,
	naam = @Naam, [reports postbeschrijving] = @ReportsPostbeschrijving 
	WHERE postId = @PostId
GO

-- Triggers voor Review
CREATE TRIGGER trigger_Review_INSERT
ON Review INSTEAD OF INSERT
AS
	DECLARE @ReviewId INT
	DECLARE @PostId INT
	DECLARE @GebruikerId INT
	DECLARE @ReviewTekst NVARCHAR(MAX)
	DECLARE @Titel NVARCHAR(MAX)

	SELECT @ReviewId = reviewId, @PostId = postId, @GebruikerId = gebruikerId, @ReviewTekst = reviewtekst, @Titel = titel FROM inserted

	DECLARE @Scheldwoorden TABLE(scheldwoordId INT, woord NVARCHAR(MAX))
	INSERT INTO @Scheldwoorden (scheldwoordId, woord) SELECT * FROM Scheldwoord

	UPDATE @Scheldwoorden
	SET @ReviewTekst=REPLACE(@ReviewTekst, woord, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(woord, 'A', '*'), 'E', '*'), 'O', '*'), 'U', '*'), 'I', '*'))
	INSERT INTO Review (postId, gebruikerId, reviewtekst, titel) VALUES(@PostId, @GebruikerId, @ReviewTekst, @Titel)
GO

CREATE TRIGGER trigger_Review_UPDATE
ON Review INSTEAD OF UPDATE
AS
	
	DECLARE @ReviewId INT
	
	DECLARE @PostId INT
	
	DECLARE @GebruikerId INT
	
	DECLARE @ReviewTekst NVARCHAR(MAX)
	DECLARE @Titel NVARCHAR(MAX)

	SELECT @ReviewId = reviewId, @PostId = postId, @GebruikerId = gebruikerId, @ReviewTekst = reviewtekst, @Titel = titel FROM inserted

	DECLARE @Scheldwoorden TABLE(scheldwoordId INT, woord NVARCHAR(MAX))
	INSERT INTO @Scheldwoorden (scheldwoordId, woord) SELECT * FROM Scheldwoord

	UPDATE @Scheldwoorden
	SET @ReviewTekst=REPLACE(@ReviewTekst, woord, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(woord, 'A', '*'), 'E', '*'), 'O', '*'), 'U', '*'), 'I', '*'))
	UPDATE Review SET postId = @PostId, gebruikerId = @GebruikerId, reviewtekst = @ReviewTekst, titel = @Titel
	WHERE reviewId = @ReviewId
GO