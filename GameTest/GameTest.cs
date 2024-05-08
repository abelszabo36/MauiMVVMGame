using MaciLaciMaui;
namespace GameTest
{
    [TestClass]
    public class GameTest
    {
        private GameTable? table;
        private GameModel? model;
        private DataAccess? persistence = new DataAccess("TextFiles");
        int[,] gameBoard = {
            {1, 0, 0, 4, 4, 4, 0},
            {0, 4, 0, 0, 0, 0, 0},
            {0, 4, 0, 0, 0, 0, 0},
            {0, 4, 0, 0, 3, 4, 4},
            {0, 0, 0, 0, 0, 0, 0},
            {0, 0, 2, 0, 4, 0, 0},
            {0, 4, 4, 4, 0, 0, 3}
        };


        [TestMethod]
        public void TestGameTableSetUp1()
        {
            int[,] matrix = { { 1, 0, 0 }, { 2, 3, 3 }, { 4, 4, 4 } };
            table = new GameTable(matrix);
            int expectedValue = 1;
            int value = table.GetField(0, 0);
            Assert.AreEqual(expectedValue, value);

            table.SetField(0, 0, 0);
            int expectedValue2 = 0;
            Assert.AreEqual(expectedValue2, table.GetField(0, 0));
        }

        [TestMethod]                                                         // Visual
        public void HeroRightMove()                                         //1 0 0 4 4 4 0      
        {
           ;                            //0 4 0 0 0 0 0
            model = new GameModel(persistence);                             //0 4 0 0 0 0 0
            model.NewGame(gameBoard);                                    //0 4 0 0 3 4 4
            model.HeroMove(0, 0, 1, "right");                                //0 0 0 0 0 0 0
            Assert.AreEqual(model.Table.GetField(0, 1), 1);                 //0 0 2 0 4 0 0
            Assert.AreEqual(model.Table.GetField(0, 0), 0);                 //0 4 4 4 0 0 3
        }
        [TestMethod]
        public void HeroLeftMove()
        {
            model = new GameModel(persistence);
            model.NewGame(gameBoard);
            // y x  size
            model.HeroMove(1, 1, 1, "left");// balra lepesnel (1,1) pozicioban helyezkedunk el        
            Assert.AreEqual(model.Table.GetField(1, 0), 1);      // megtortenik az egyel balra lepes          
            Assert.AreEqual(model.Table.GetField(1, 1), 0);      // a regi hely felszabadul          
        }

        [TestMethod]
        public void HeroMoveUp()
        {
            model = new GameModel(persistence);
            model.NewGame(gameBoard);
            model.HeroMove(2, 3, 1, "up"); // a harmadik sorba es a masodik oszlopba helyezkedunk el, 
            Assert.AreEqual(model.Table.GetField(1, 3), 1);           // egyel fel lepesnel a masodik sorba es a masodik oszlopba leszzunk
           Assert.AreEqual(model.Table.GetField(2, 3), 0);
        }

        [TestMethod]
        public void HeroMoveDown()
        {
            model = new GameModel(persistence);
            model.NewGame(gameBoard);
            model.HeroMove(2, 2, 1, "down"); // az masodik sorba es masodik oszlopba helyezkedunk el, 
            Assert.AreEqual(model.Table.GetField(3, 2), 1);           // egyel le lepesnel a harmadik sorba es a masodik oszlopba leszzunk
            Assert.AreEqual(model.Table.GetField(2, 2), 0);
        }

        [TestMethod]
        public void HeroMeetWall()
        {
            model = new GameModel(persistence);
            model.NewGame(gameBoard);
            model.HeroMove(0, 0, 1, "down");
            Assert.AreEqual(model.Table.GetField(1, 0), 1);
            model.HeroMove(0, 1, 1, "right");
            Assert.AreEqual(model.Table.GetField(1, 0), 1);
            Assert.AreEqual(model.Table.GetField(1, 1), 4);
        }


        [TestMethod]
        public void CollectBaskett()
        {
            model = new GameModel(persistence);
            model.NewGame(gameBoard);
            Assert.AreEqual(model.Table.GetField(3, 4), 3); // a (3,4) mezobe kosar van
            Assert.AreEqual(model.Collected, 0); // kezdetben nincs begyujtott kosarunk
            model.HeroMove(3, 3, 1, "right"); // a hos a kosar jobb oldalan helyezkedik el es ha egyet jobra lep akkor begyujti
            Assert.AreEqual(model.Collected, 1);
        }

        [TestMethod]
        public void EnemyMoveTest()
        {
            model = new GameModel(persistence);
            model.NewGame(gameBoard);// ez az enemy fel le mozog
            for (int i = 5; i >= 0; --i) // elmegy az enemy a palya tetejebe
            {
                Assert.AreEqual(model.Table.GetField(i, 2), 2);
                model.EnemyMove();
            }

            for (int i = 0; i <= 5; ++i) // majd megfordul es jon vissza
            {
                Assert.AreEqual(model.Table.GetField(i, 2), 2);
                model.EnemyMove();
            }
        }



    }
}