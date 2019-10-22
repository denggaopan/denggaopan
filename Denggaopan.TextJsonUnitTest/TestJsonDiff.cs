using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Denggaopan.TestJsonUnitTest
{
    [TestClass]
    public class TestJsonDiff
    {
        [TestMethod]
        [Description(description: "�����������л�")]
        public void TestNumber()
        {
            object jsonObject = new { number = 123.456 };
            string aJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(value: jsonObject);
            string bJsonString = System.Text.Json.JsonSerializer.Serialize(value: jsonObject,
                options: new System.Text.Json.JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            Assert.AreEqual(expected: aJsonString, actual: bJsonString, message: "�����������л�ʧ��");
        }

        [TestMethod]
        [Description(description: "����Ӣ�����л�")]
        public void TestEnglish()
        {
            object jsonObject = new { english = "bla bla" };
            string aJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(value: jsonObject);
            string bJsonString = System.Text.Json.JsonSerializer.Serialize(value: jsonObject,
                options: new System.Text.Json.JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            Assert.AreEqual(expected: aJsonString, actual: bJsonString, message: "����Ӣ�����л�ʧ��");
        }

        [TestMethod]
        [Description(description: "�����������л�")]
        public void TestChinese()
        {
            object jsonObject = new { chinese = "�ҳ���׼�Ĳ��˷�" };
            string aJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(value: jsonObject);
            string bJsonString = System.Text.Json.JsonSerializer.Serialize(value: jsonObject,
                options: new System.Text.Json.JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            Assert.AreEqual(expected: aJsonString, actual: bJsonString, message: "�����������л�ʧ��");
        }

        [TestMethod]
        [Description(description: "����Ӣ�ķ���")]
        public void TestEnglishSymbol()
        {
            object jsonObject = new { symbol = @"~`!@#$%^&*()_-+={}[]:;'<>,.?/ " };
            string aJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(value: jsonObject);
            string bJsonString = System.Text.Json.JsonSerializer.Serialize(value: jsonObject,
                options: new System.Text.Json.JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            Assert.AreEqual(expected: aJsonString, actual: bJsonString, message: "����Ӣ�ķ���ʧ��");
        }

        [TestMethod]
        [Description(description: "�������ķ���")]
        public void TestChineseSymbol()
        {
            object jsonObject = new { chinese_symbol = @"~��@#��%����&*������-+=��������������������������������" };
            string aJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(value: jsonObject);
            string bJsonString = System.Text.Json.JsonSerializer.Serialize(value: jsonObject,
                options: new System.Text.Json.JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            Assert.AreEqual(expected: aJsonString, actual: bJsonString, message: "�������ķ���ʧ��");
        }

        [TestMethod]
        [Description(description: "���Է����л���ֵ�ַ�����ʽת��Ϊ��ֵ����")]
        public void TestDeserializeNumber()
        {
            string ajsonString = "{\"Number\":\"123\"}";

            TestClass aJsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<TestClass>(ajsonString);

            // ����The JSON value could not be converted to System.Int32. Path: $.number | LineNumber: 0 | BytePositionInLine: 15
            TestClass bJsonObject = System.Text.Json.JsonSerializer.Deserialize<TestClass>(json: ajsonString,
                options: new System.Text.Json.JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            Assert.AreEqual(expected: aJsonObject.Number, actual: bJsonObject.Number, message: "���Է����л���ֵ�ַ�����ʽת��Ϊ��ֵ����ʧ��");
        }

        public class TestClass
        {
            public int Number { get; set; }
        }
    }
}
