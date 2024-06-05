# CleanCodeLaboration

-Flytta ut allt från Main över till en separat GameEngine class
-Tänker bryta ner allt i olika kategorier, och skapa classer/interfaces ut ifrån det. T.ex 
	- GameEngine(Cartridge spel) -> som er själva konsolen som spelar spelet man injekterar inn spelet
	- Cartridge(Game game) -> Tanken här är att här slår man ihop GameLogic() med representerade highscore data från HighScoreService()
		- HighScoreService(Game game) -> Ska innehålla highscore data för spelet, som blir lagrat på filer som en DbMock 
		- GameLogic() -> Spelets Logic
		- PlayerService()

## GameLogic()

*Tanke:* Ska hantera all spel logic, 	


## HighScoreService(IGameLogic game)

*Tanke:* Den ska sköta kopplingen mellan spelet och highscore, tar in en _IGameLogic_ som DI för att få inn Id:t på spelet
och hämta highscoren beroende på vilket spel man injekterar.

- #### Skapade metoder

	- *InitialLoadHighScores* -> Letar upp om det finns en fil som stämmer med spelets highScoreId och laddar upp det i en lista
	- *AddHighScore* -> Bryter ner en HighScoreForm för att kunna skriva inn den i filen
	- *GetAllUserHighScore* -> Hämtar alla highscores som tillhör användaren		
	- *GetAllHighScores* -> Hämtar alla highscores						

- #### Skapade klasser för HighScoreService

	- *HighScoreForm* -> En form för att hantera higescores.

## PlayerService()

*Tanke:* Ska hantera alla uppgifter om spelaren

## Cartridge()

*Tanke:* 

