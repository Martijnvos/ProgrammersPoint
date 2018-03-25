CREATE PROCEDURE RecordCategoryVisitProcedure(
	@Categorie INT,
	@GebruikerId INT
) AS
	DECLARE @AantalBekeken INT

	IF EXISTS (SELECT [aantal bekeken] FROM Gebruiker_Categorie WHERE gebruikerId = @GebruikerId AND categorie = @Categorie)
	BEGIN
		SET @AantalBekeken = (SELECT [aantal bekeken] FROM Gebruiker_Categorie WHERE gebruikerId = @GebruikerId AND categorie = @Categorie)
		UPDATE Gebruiker_Categorie SET [aantal bekeken] = (@AantalBekeken + 1) WHERE gebruikerId = @GebruikerId AND categorie = @Categorie
	END
	ELSE
		INSERT INTO Gebruiker_Categorie (gebruikerId, categorie) --Aantal bekeken heeft default 1 
		VALUES(@GebruikerId, @Categorie) 
GO

CREATE PROCEDURE GetInteressantePostsProcedure(
	@GebruikerId INT
) AS
	DECLARE @SpecifiekeCategorie INT
	DECLARE @PersoonlijkeCategorieInfo TABLE(categorie INT, [aantal bekeken] INT)
	DECLARE @InteressantePosts TABLE(postId INT, categorie INT, [beschrijving taal] NVARCHAR(MAX), 
					[laatst geüpdatet] DATETIME, postversie INT, taalversie NVARCHAR(MAX),
					[moeilijkheidsgraad onderwerp] TINYINT, naam NVARCHAR(MAX), [reports postbeschrijving] INT)	
	DECLARE @PersoonlijkeCategorieInfoCursor AS CURSOR
	SET @PersoonlijkeCategorieInfoCursor = CURSOR FOR SELECT categorie FROM @PersoonlijkeCategorieInfo

	INSERT INTO @PersoonlijkeCategorieInfo (categorie, [aantal bekeken]) SELECT categorie, [aantal bekeken] FROM Gebruiker_Categorie
		WHERE gebruikerId = @GebruikerId ORDER BY [aantal bekeken]

	IF EXISTS(SELECT categorie FROM Gebruiker_Categorie WHERE gebruikerId = @GebruikerId)
	BEGIN
		OPEN @PersoonlijkeCategorieInfoCursor
		FETCH NEXT FROM @PersoonlijkeCategorieInfoCursor INTO @SpecifiekeCategorie
		WHILE @@FETCH_STATUS = 0
 		BEGIN
			IF ((SELECT COUNT(*) FROM Post WHERE categorie = @SpecifiekeCategorie) < 2)
			BEGIN
 				INSERT INTO @InteressantePosts SELECT * FROM Post WHERE categorie = @SpecifiekeCategorie
 				FETCH NEXT FROM @PersoonlijkeCategorieInfoCursor INTO @SpecifiekeCategorie
			END
			ELSE
			INSERT INTO @InteressantePosts SELECT TOP 2 * FROM Post WHERE categorie = @SpecifiekeCategorie
 				FETCH NEXT FROM @PersoonlijkeCategorieInfoCursor INTO @SpecifiekeCategorie
 		END
		CLOSE @PersoonlijkeCategorieInfoCursor;
 		DEALLOCATE @PersoonlijkeCategorieInfoCursor;

		SELECT * FROM @InteressantePosts
	END
GO