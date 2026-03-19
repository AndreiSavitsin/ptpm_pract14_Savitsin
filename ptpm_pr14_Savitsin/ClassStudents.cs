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

        static public List<ClassStudents> GetList()
        {
            return ListStudents;
        }

        static public void AddStudent(ClassStudents Student)
        {
            ListStudents.Add(Student);
        }

        static public void RemoveStudent(int index)
        {
            ListStudents.RemoveAt(index);
        }
        static public ClassStudents GetStudents(int index)
        {
            return ListStudents[index];
        }
        static public int CountStudents()
        {
            return ListStudents.Count;
        }
        static public void ClearStudents()
        {
            ListStudents.Clear();
        }

        public string ToString()
        {
            return Surname + ";" + Name + ";" + Birthday + ";" + PhoneNumber + ";" + Adress + ";" + PhotoUrl;
        }

        public static ClassStudents FromString(string line)
        {
            string[] parts = line.Split(';');
            if (parts.Length < 6)
            {
                return null;
            }
            DateTime date;
            if (!DateTime.TryParse(parts[2], out date))
            {
                date = DateTime.Now;
            }
            return new ClassStudents(parts[0], parts[1], date, parts[3], parts[4], parts[5]);
            
        }

        static public void SaveToFile(string filePath)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    for (int i = 0; i < ListStudents.Count; i++)
                    {
                        sw.WriteLine(ListStudents[i].ToString());
                    }
                }
                MessageBox.Show($"Сохранено в {Directory.GetCurrentDirectory()}", "Сохранение");
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ошибка сохранения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        static public void LoadFromFile(string filePath)
        {
            try
            {
                ListStudents.Clear();
                using (StreamReader sr = File.OpenText(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] parts = line.Split(';');
                        if (parts.Length >= 6)
                        {
                            DateTime date;
                            if (!DateTime.TryParse(parts[2], out date))
                            {
                                date = DateTime.Now;
                                ListStudents.Add(new ClassStudents(parts[0], parts[1], date, parts[3], parts[4], parts[5]));
                            }
                        }
                    }
                    MessageBox.Show("Файл загружен");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
