declare @response int
EXEC UserLogin @userName = 'string', @userPassword = 'string', @retVal = @response OUTPUT;

drop proc [UserLogin]
 CREATE PROCEDURE [UserLogin]
      @userName Nvarchar(50),
      @userPassword nvarchar(200),
      @retVal int OUTPUT
      AS
  BEGIN
      SET NOCOUNT ON;
         Select 
                username,
                name,
                isAdmin
                FROM [AccountUser] 
                where [userName] = @userName and 
               [userPassword] = hashbytes('sha2_512', @userPassword + cast([salt] as nvarchar(36)))
         IF(@@ROWCOUNT > 0)
         BEGIN
          SET @retVal = 200
         END
         ELSE
         BEGIN
           SET @retVal = 500
         END
  END