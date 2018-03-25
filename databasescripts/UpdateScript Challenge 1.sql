CREATE PROCEDURE PostInsertProcedure(
	@Categorie INT,
	@BeschrijvingTaal NVARCHAR(MAX),
	@MoeilijkheidsgraadOnderwerp TINYINT,
	@TaalVersie NVARCHAR(MAX),
	@Naam NVARCHAR(MAX)
) AS
	INSERT INTO Post (categorie, [beschrijving taal], [moeilijkheidsgraad onderwerp], taalversie, naam) 
	VALUES(@Categorie, @BeschrijvingTaal, @MoeilijkheidsgraadOnderwerp, @TaalVersie, @Naam)
GO

CREATE PROCEDURE PostGetNieuwePostsProcedure(
	@DateTimeVerschil INT
) AS
	SELECT *
	FROM Post p
	WHERE p.[laatst geüpdatet] > DATEADD(hour, @DateTimeVerschil, GETDATE())
GO