using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleTodoXForms.Models;

namespace SampleTodo.Test
{
    [TestClass]
    public class TodoTest
    {
        /// <summary>
        /// 簡単なチェック
        /// </summary>
        [TestMethod]
        public void TestInit()
        {
            var todo = new ToDo();
            todo.Id = 10;
            todo.Text = "test item";
            todo.DueDate = new DateTime(2017, 5, 1);
            todo.Completed = false;
            todo.CreatedAt = new DateTime(2017, 4, 1);

            Assert.AreEqual(10, todo.Id);
            Assert.AreEqual("test item", todo.Text);
            Assert.AreEqual(new DateTime(2017, 5, 1), todo.DueDate);

            // 期日のフォーマットチェック
            Assert.AreEqual(true, todo.UseDueDate);
            Assert.AreEqual(new DateTime(2017, 5, 1), todo.DispDueDate);
            Assert.AreEqual("2017-05-01", todo.StrDueDate);
        }

        /// <summary>
        /// 期日が未指定（null）の場合
        /// </summary>
        [TestMethod]
        public void TestDueDateIsNull()
        {
            var todo = new ToDo();
            todo.Id = 10;
            todo.Text = "test item";
            todo.DueDate = null;
            todo.Completed = false;
            todo.CreatedAt = new DateTime(2017, 4, 1);

            Assert.AreEqual(10, todo.Id);
            Assert.AreEqual("test item", todo.Text);
            Assert.AreEqual(null, todo.DueDate);

            // 期日のフォーマットチェック
            Assert.AreEqual(false, todo.UseDueDate);
            Assert.IsNotNull(todo.DispDueDate);         // null を返さない
            Assert.AreEqual("", todo.StrDueDate);       // 空欄を返す
        }

        /// <summary>
        /// コピーのチェック（新規）
        /// </summary>
        [TestMethod]
        public void TestCopy1()
        {
            var todo = new ToDo();
            todo.Id = 10;
            todo.Text = "test item";
            todo.DueDate = new DateTime(2017, 5, 1);
            todo.Completed = true;
            todo.CreatedAt = new DateTime(2017, 4, 1);

            // 新しいオブジェクトを作る
            var item = todo.Copy();
            Assert.AreEqual(10, item.Id);
            Assert.AreEqual("test item", item.Text);
            Assert.AreEqual(new DateTime(2017, 5, 1), item.DueDate);
            Assert.AreEqual(true, item.Completed);
            Assert.AreEqual(new DateTime(2017, 4, 1), item.CreatedAt);
        }

        /// <summary>
        /// コピーのチェック（ターゲットあり）
        /// </summary>
        [TestMethod]
        public void TestCopy2()
        {
            var todo = new ToDo();
            todo.Id = 10;
            todo.Text = "test item";
            todo.DueDate = new DateTime(2017, 5, 1);
            todo.Completed = true;
            todo.CreatedAt = new DateTime(2017, 4, 1);

            // ターゲットを指定する
            var item = new ToDo();
            todo.Copy(item);
            Assert.AreEqual(10, item.Id);
            Assert.AreEqual("test item", item.Text);
            Assert.AreEqual(new DateTime(2017, 5, 1), item.DueDate);
            Assert.AreEqual(true, item.Completed);
            Assert.AreEqual(new DateTime(2017, 4, 1), item.CreatedAt);
        }
    }
}
