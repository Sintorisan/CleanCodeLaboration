# CleanCodeLaboration

-Flytta ut allt fr�n Main �ver till en separat GameEngine class
-T�nker bryta ner allt i olika kategorier, och skapa classer/interfaces ut ifr�n det. T.ex 
	- GameEngine(Cartridge spel) -> som er sj�lva konsolen som spelar spelet man injekterar inn spelet
	- Cartridge(Game game) -> Tanken h�r �r att h�r sl�r man ihop GameLogic() med representerade highscore data fr�n HighScoreService()
		- HighScoreService(Game game) -> Ska inneh�lla highscore data f�r spelet, som blir lagrat p� filer som en DbMock 
		- GameLogic() -> Spelets Logic
		- PlayerService()

## GameLogic()

*Tanke:* Ska hantera all spel logic, 	


## HighScoreService(IGameLogic game)

*Tanke:* Den ska sk�ta kopplingen mellan spelet och highscore, tar in en _IGameLogic_ som DI f�r att f� inn Id:t p� spelet
och h�mta highscoren beroende p� vilket spel man injekterar.

- #### Skapade metoder

	- *InitialLoadHighScores* -> Letar upp om det finns en fil som st�mmer med spelets highScoreId och laddar upp det i en lista
	- *AddHighScore* -> Bryter ner en HighScoreForm f�r att kunna skriva inn den i filen
	- *GetAllUserHighScore* -> H�mtar alla highscores som tillh�r anv�ndaren		
	- *GetAllHighScores* -> H�mtar alla highscores						

- #### Skapade klasser f�r HighScoreService

	- *HighScoreForm* -> En form f�r att hantera higescores.

## PlayerService()

*Tanke:* Ska hantera alla uppgifter om spelaren

## Cartridge()

*Tanke:* 

