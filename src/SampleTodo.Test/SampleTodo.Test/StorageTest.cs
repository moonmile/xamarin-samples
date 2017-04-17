using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SampleTodoXForms.Models;

namespace SampleTodo.Test
{
    [TestClass]
    public class StorageTest
    {
        /// <summary>
        /// シリアライズ＆デシリアライズのテスト
        /// </summary>
        [TestMethod]
        public void TestSave()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 3), Completed = false });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 2), Completed = true });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            var items = new ToDoFiltableCollection(lst);
            // シリアライズする
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(items);
            // 空白ではない
            Assert.AreNotEqual("", data);

            // デシリアライズする
            var newItems = Newtonsoft.Json.JsonConvert.DeserializeObject<ToDoFiltableCollection>(data);
            Assert.AreEqual(3, newItems.Count);
            Assert.AreEqual(1, newItems[0].Id);
            Assert.AreEqual("ccc", newItems[0].Text);
            Assert.AreEqual(new DateTime(2017, 5, 1), newItems[0].DueDate);
            Assert.AreEqual(new DateTime(2017, 4, 3), newItems[0].CreatedAt);
            Assert.AreEqual(false, newItems[0].Completed);
        }

        /// <summary>
        /// 期日（DueDate）がNULLの場合
        /// </summary>
        [TestMethod]
        public void TestDueDateIsNull()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "aaa", DueDate = null, CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            var items = new ToDoFiltableCollection(lst);
            // シリアライズする
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(items);
            System.Diagnostics.Debug.WriteLine(data);
            // 空白ではない
            Assert.AreNotEqual("", data);

            // デシリアライズする
            var newItems = Newtonsoft.Json.JsonConvert.DeserializeObject<ToDoFiltableCollection>(data);
            Assert.AreEqual(1, newItems.Count);
            Assert.AreEqual(1, newItems[0].Id);
            Assert.AreEqual("aaa", newItems[0].Text);
            Assert.AreEqual(null, newItems[0].DueDate);
            Assert.AreEqual(new DateTime(2017, 4, 1), newItems[0].CreatedAt);
            Assert.AreEqual(false, newItems[0].Completed);
        }

        /// <summary>
        /// 項目がゼロ件の場合
        /// </summary>
        [TestMethod]
        public void TestCountIsZero()
        {
            var lst = new List<ToDo>();
            var items = new ToDoFiltableCollection(lst);
            // シリアライズする
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(items);
            System.Diagnostics.Debug.WriteLine(data);
            // 空白ではない
            Assert.AreNotEqual("", data);

            // デシリアライズする
            var newItems = Newtonsoft.Json.JsonConvert.DeserializeObject<ToDoFiltableCollection>(data);
            Assert.AreEqual(0, newItems.Count);
        }

        /// <summary>
        /// XML形式でのシリアライズ
        /// </summary>
        [TestMethod]
        public void TestSaveXml()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 3), Completed = false });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 2), Completed = true });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            var items = new ToDoFiltableCollection(lst);
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(ToDoFiltableCollection));

            var sw = new System.IO.StringWriter();
            xs.Serialize(sw, items);
            var xml = sw.ToString();

            var sr = new System.IO.StringReader(xml);
            var newItems = xs.Deserialize(sr) as ToDoFiltableCollection;
            Assert.AreEqual(3, newItems.Count);
            Assert.AreEqual(1, newItems[0].Id);
            Assert.AreEqual("ccc", newItems[0].Text);
            Assert.AreEqual(new DateTime(2017, 5, 1), newItems[0].DueDate);
            Assert.AreEqual(new DateTime(2017, 4, 3), newItems[0].CreatedAt);
            Assert.AreEqual(false, newItems[0].Completed);
        }
        /// <summary>
        /// XML形式でのシリアライズ
        /// </summary>
        [TestMethod]
        public void TestSaveXmlDueDateIsNull()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "aaa", DueDate = null, CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            var items = new ToDoFiltableCollection(lst);
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(ToDoFiltableCollection));

            var sw = new System.IO.StringWriter();
            xs.Serialize(sw, items);
            var xml = sw.ToString();

            var sr = new System.IO.StringReader(xml);
            var newItems = xs.Deserialize(sr) as ToDoFiltableCollection;
            Assert.AreEqual(1, newItems.Count);
            Assert.AreEqual(1, newItems[0].Id);
            Assert.AreEqual("aaa", newItems[0].Text);
            Assert.AreEqual(null, newItems[0].DueDate);
            Assert.AreEqual(new DateTime(2017, 4, 1), newItems[0].CreatedAt);
            Assert.AreEqual(false, newItems[0].Completed);
        }

        /// <summary>
        /// ストレージへのシリアライズ
        /// </summary>
        [TestMethod]
        public void TestSaveStream()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 3), Completed = false });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 2), Completed = true });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            var items = new ToDoFiltableCollection(lst);

            var sw = System.IO.File.OpenWrite("save.xml");
            bool b = items.Save(sw);
            sw.Close();
            Assert.AreEqual(true, b);

            /// 新しいコレクションを用意する
            var newItems = new ToDoFiltableCollection();
            var sr = System.IO.File.OpenRead("save.xml");
            b = newItems.Load(sr);
            sr.Close();
            Assert.AreEqual(true, b);
            Assert.AreEqual(3, newItems.Count);
            Assert.AreEqual(1, newItems[0].Id);
            Assert.AreEqual("ccc", newItems[0].Text);
            Assert.AreEqual(new DateTime(2017, 5, 1), newItems[0].DueDate);
            Assert.AreEqual(new DateTime(2017, 4, 3), newItems[0].CreatedAt);
            Assert.AreEqual(false, newItems[0].Completed);

            System.IO.File.Delete("save.xml");
        }
    }
}
