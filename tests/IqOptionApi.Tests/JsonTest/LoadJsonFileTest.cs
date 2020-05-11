using System;

namespace IqOptionApi.Tests.JsonTest {
    public class LoadJsonFileTest : IDisposable {



        public LoadJsonFileTest() {

        }
        public string LoadJson(string fileName) {

            var JSON = System.IO.File.ReadAllText($"{fileName}");
            return JSON;
        }

        public void Dispose() {

        }
    }
}