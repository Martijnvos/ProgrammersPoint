CREATE PROCEDURE ReviewWaarderingInsertOrUpdate(
	@GebruikerID INT,
	@ReviewID INT,
	@Upvote BIT,
	@Downvote BIT,
	@Report BIT
) AS
	DECLARE @AantalUpvotes INT;
 	DECLARE @AantalDownvotes INT;
 	DECLARE @AantalReports INT;
	
	IF EXISTS (SELECT gebruikerId, reviewId FROM ReviewWaardering WHERE gebruikerId = @GebruikerID AND reviewId = @ReviewID)
		BEGIN	
			UPDATE ReviewWaardering SET upvote = @Upvote,
                	downvote = @Downvote,
                	report = @Report
                	WHERE gebruikerId = @GebruikerID AND reviewId = @ReviewID
		END
	ELSE
		BEGIN
			INSERT INTO ReviewWaardering (gebruikerId, reviewId, upvote, downvote, report) VALUES
                	(@gebruikerId, @reviewId, @upvote, @downvote, @report)
		END
GO