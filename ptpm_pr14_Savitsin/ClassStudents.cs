using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;

namespace ptpm_pr9_Savitsin
{
    internal class ClassStudents
    {
        static List<ClassStudents> ListStudents = new List<ClassStudents>(); //Лист студентов

        string surname;
        string name;
        DateTime birthday;
        string phoneNumber;
        string adress;
        string photoUrl;

        public ClassStudents(string surname, string name, DateTime birthday, string phoneNumber, string adress, string photoUrl)
        {
            this.surname = surname;
            this.name = name;
            this.birthday = birthday;
            this.phoneNumber = phoneNumber;
            this.adress = adress;
            this.photoUrl = photoUrl;
        }

        public ClassStudents(string surname, string name)
        {
            this.surname = surname;
            this.name = name;
        }

        static public List<ClassStudents> GetList()
        {
            return ListStudents;
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Name
        { 
            get { return name; }
            set { name = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Adress
        {
            get { return adress; }
            set { adress = value; }
        }

        public string PhotoUrl
        {
            get { return photoUrl; }
            set { photoUrl = value; }
        }

        static public void AddStudent(ClassStudents Student)
        {
            ClassStudents.ListStudents.Add(Student);
        }

        static public void RemoveStudent(int index)
        {
            for (int i = 0; i < ClassStudents.ListStudents.Count; i++)
            {
                if (i == index)
                {
                    ClassStudents.ListStudents.RemoveAt(i);
                }
            }
        }
        static public ClassStudents GetStudents(int index)
        {
            return ClassStudents.ListStudents[index];
        }
        static public int CountStudents()
        {
            return ClassStudents.ListStudents.Count;
        }
        static public void ClearStudents()
        {
            ClassStudents.ListStudents.Clear();
        }
        static public void SaveToFile(string filePath)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                foreach (var item in ClassStudents.ListStudents)
                {
                    sw.WriteLine(item);
                }
            }
            MessageBox.Show($"Сохранено в {Directory.GetCurrentDirectory()}", "Сохранение");
        }
        static public void LoadFromFile(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                ClassStudents.ListStudents.Clear();
                while (!sr.EndOfStream)
                {
                    ClassStudents.ListStudents.Add(sr.ReadLine());
                }
                MessageBox.Show("Файл загружен");
            }
        }
    }
}
