using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SampleTodoXForms.Models;

namespace SampleTodo.Test
{
    [TestClass]
    public class TodoCollectionTest
    {
        /// <summary>
        /// 簡単なテスト
        /// </summary>
        [TestMethod]
        public void TestInit()
        {
            var items = new ToDoFiltableCollection();
            Assert.AreEqual(0, items.Count);

            items.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 3) });
            Assert.AreEqual(1, items.Count);
            items.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 2) });
            Assert.AreEqual(2, items.Count);
            items.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1) });
            Assert.AreEqual(3, items.Count);

            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 3) });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 2) });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1) });
            items = new ToDoFiltableCollection(lst);
            Assert.AreEqual(3, items.Count);
        }

        /// <summary>
        /// 完了フラグでフィルターする
        /// </summary>
        [TestMethod]
        public void TestFilter()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 3), Completed = false });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 2), Completed = true });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            var items = new ToDoFiltableCollection(lst);

            Assert.AreEqual(3, items.Count);
            // 完了を表示しない
            items.SetFilter(false, 0);
            Assert.AreEqual(2, items.Count);
            // 完了を表示する
            items.SetFilter(true, 0);
            Assert.AreEqual(3, items.Count);
        }


        /// <summary>
        /// 作成日でソートする
        /// </summary>
        [TestMethod]
        public void TestSortByCreatedAt()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 2), Completed = true });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 3), Completed = false });
            var items = new ToDoFiltableCollection(lst);

            // 作成日順にソートする
            items.SetFilter(true, 0);
            Assert.AreEqual(3, items.Count);
            // 新しい順にソートされる
            Assert.AreEqual("aaa", items[0].Text);
            Assert.AreEqual("bbb", items[1].Text);
            Assert.AreEqual("ccc", items[2].Text);
        }

        /// <summary>
        /// 項目名でソートする
        /// </summary>
        [TestMethod]
        public void TestSortByText()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 2), Completed = true });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 3), Completed = false });
            var items = new ToDoFiltableCollection(lst);

            // 項目名順にソートする
            items.SetFilter(true, 1);
            Assert.AreEqual(3, items.Count);
            // 辞書順にソートされる
            Assert.AreEqual("aaa", items[0].Text);
            Assert.AreEqual("bbb", items[1].Text);
            Assert.AreEqual("ccc", items[2].Text);
        }

        /// <summary>
        /// 期日でソートする
        /// </summary>
        [TestMethod]
        public void TestSortByDueDate()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 2), Completed = true });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 3), Completed = false });
            var items = new ToDoFiltableCollection(lst);

            // 期日順にソートする
            items.SetFilter(true, 2);
            Assert.AreEqual(3, items.Count);
            // 期日が近い順（日付順）にソートされる
            Assert.AreEqual("bbb", items[0].Text);
            Assert.AreEqual("aaa", items[1].Text);
            Assert.AreEqual("ccc", items[2].Text);
        }

        /// <summary>
        /// 項目の追加時に自動でソートされる
        /// 作成順の場合
        /// </summary>
        [TestMethod]
        public void TestAdd1()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 2), Completed = true });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 3), Completed = false });
            var items = new ToDoFiltableCollection(lst);

            // 作成日順にソートする
            items.SetFilter(true, 0);
            Assert.AreEqual(3, items.Count);
            Assert.AreEqual("aaa", items[0].Text);
            // 新しい項目を追加する
            items.Add(new ToDo() { Id = 100, Text = "new", DueDate = null, CreatedAt = new DateTime(2017, 5, 1), Completed = false });
            // 追加した項目が先頭になる
            Assert.AreEqual(4, items.Count);
            Assert.AreEqual("new", items[0].Text);
        }
        /// <summary>
        /// 項目の追加時に自動でソートされる
        /// 項目名順の場合
        /// </summary>
        [TestMethod]
        public void TestAdd2()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 2), Completed = true });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 3), Completed = false });
            var items = new ToDoFiltableCollection(lst);

            // 項目名順にソートする
            items.SetFilter(true, 1);
            Assert.AreEqual(3, items.Count);
            Assert.AreEqual("aaa", items[0].Text);
            Assert.AreEqual("bbb", items[1].Text);
            Assert.AreEqual("ccc", items[2].Text);
            // 新しい項目を追加する
            items.Add(new ToDo() { Id = 100, Text = "abc", DueDate = null, CreatedAt = new DateTime(2017, 5, 1), Completed = false });
            // 追加した項目が2番目になる
            Assert.AreEqual(4, items.Count);
            Assert.AreEqual("aaa", items[0].Text);
            Assert.AreEqual("abc", items[1].Text);
            Assert.AreEqual("bbb", items[2].Text);
            Assert.AreEqual("ccc", items[3].Text);
        }
        /// <summary>
        /// 項目の追加時に自動でソートされる
        /// 期日順の場合
        /// </summary>
        [TestMethod]
        public void TestAdd3()
        {
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "ccc", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 4, 1), Completed = false });
            lst.Add(new ToDo() { Id = 2, Text = "bbb", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 4, 2), Completed = true });
            lst.Add(new ToDo() { Id = 3, Text = "aaa", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 4, 3), Completed = false });
            var items = new ToDoFiltableCollection(lst);

            // 期日順にソートする
            items.SetFilter(true, 2);
            Assert.AreEqual(3, items.Count);
            Assert.AreEqual("bbb", items[0].Text);
            Assert.AreEqual("aaa", items[1].Text);
            Assert.AreEqual("ccc", items[2].Text);
            // 新しい項目を追加する
            items.Add(new ToDo() { Id = 100, Text = "new", DueDate = new DateTime(2017, 5, 10), CreatedAt = new DateTime(2017, 5, 1), Completed = false });
            // 追加した項目が最後になる
            Assert.AreEqual(4, items.Count);
            Assert.AreEqual("bbb", items[0].Text);
            Assert.AreEqual("aaa", items[1].Text);
            Assert.AreEqual("ccc", items[2].Text);
            Assert.AreEqual("new", items[3].Text);
        }
    }
}
