namespace Brainfuck_interpretator
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void Check2()
        {
            binary test = new binary();
            var currentResult = test.Execute("++++++++++++++++++++++++++++++++++++++++++++++++++.");
            var expectedResut = "2";
            Assert.AreEqual(expectedResut, currentResult);
        }

        [TestMethod]
        public void simpleCycle()
        {
            binary test = new binary();
            var currentResult = test.Execute("+++++ [ > ++++++++ < - ]  В ячейке #1 теперь 40" +
                "\r\n> ++++++++               Добавили еще 8, в ячейке #1 теперь 48 ('0')" +
                "\r\n< +++++                   В ячейке #0 теперь 5 (счетчик)" +
                "\r\n[ > + . < - ]             ЦИКЛ: перешли к 48, сделали 49 ('1'), напечатали, вернулись, уменьшили 5\r\n");
            var expectedResut = "12345";
            Assert.AreEqual(expectedResut, currentResult);
        }
        [TestMethod]
        public void doubleCycle()
        {
            binary test = new binary();
            var currentResult = test.Execute("++[>++[>+<-]<-]>>++++++[<++++++++>-]<.");
            var expectedResut = "2";
            Assert.AreEqual(expectedResut, currentResult);
        }

        [TestMethod]
        public void Cycle()
        {
            binary test = new binary();
            var currentResult = test.Execute("++++++ [ > ++++++++ < - ] > ++ .");
            var expectedResut = "2";
            Assert.AreEqual(expectedResut, currentResult);
        }
        [TestMethod]
        public void neverEndCycle()
        {
            binary test = new binary();
            var expectedResut = "Обнаружен бесконечный цикл на позиции 7.";
            try
            {
                var currentResult = test.Execute("++++++ [ > ++++++++ < ] > ++ .");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(expectedResut, ex.Message);
            }
        }
        [TestMethod]
        public void NestedCycles_ShouldWork()
        {
            // Вложенные циклы: 3 * (3 * 5) = 45 (символ '-')
            binary test = new binary();
            var result = test.Execute("+++[>+++[>+++++<-]<-]>>.");
            Assert.AreEqual("-", result);
        }

        [TestMethod]
        public void CellOverflow_ShouldWrapAround()
        {
            // Проверка переполнения byte: 0 - 1 = 255. 
            // В ASCII 255 — это специальный символ, проверим через длину или код
            binary test = new binary();
            var result = test.Execute("-.");
            Assert.AreEqual((char)255, result[0]);
        }

        [TestMethod]
        public void PointerMovement_ShouldHandleMultipleCells()
        {
            // Записываем 1 в первую ячейку, 2 во вторую, выводим вторую потом первую
            // Ожидаем ASCII коды 2 и 1 (не печатные, но для теста сойдут)
            binary test = new binary();
            var result = test.Execute("+ > ++ . < .");
            Assert.AreEqual($"{(char)2,1}{(char)1,1}", result);
        }

        [TestMethod]
        public void ClearLoop_ShouldSetZero()
        {
            // Популярная конструкция [-] которая обнуляет ячейку
            binary test = new binary();
            // Устанавливаем 50 ('2'), потом обнуляем, прибавляем 49 ('1')
            var result = test.Execute("++++++++++++++++++++++++++++++++++++++++++++++++++ [-] ++++++++++++++++++++++++++++++++++++++++++++++++++.");
            // Ошибка в расчете выше, 49 это '1'. Исправим:
            var result2 = test.Execute("++++++++++++++++++++++++++++++++++++++++++++++++++[-]+++++++++++++++++++++++++++++++++++++++++++++++++.");
            Assert.AreEqual("1", result2);
        }

        [TestMethod]
        public void ComplexInfiniteCycle_ShouldBeDetected()
        {
            // Цикл, который меняет соседнюю ячейку, но забывает менять счетчик (позиция 0)
            binary test = new binary();
            var expectedMessage = "Обнаружен бесконечный цикл на позиции 1.";
            try
            {
                test.Execute("+[>+<]"); // На позиции 1 стоит '['
                Assert.Fail("Исключение не было выброшено");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }

        [TestMethod]
        public void EmptyCode_ShouldReturnEmptyString()
        {
            binary test = new binary();
            var result = test.Execute("");
            Assert.AreEqual("", result);
        }


    }
}
