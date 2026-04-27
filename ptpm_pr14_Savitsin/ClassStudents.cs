using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.Json;

namespace ptpm_pr9_Savitsin
{
    internal class ClassStudents
    {
        static List<ClassStudents> ListStudents = new List<ClassStudents>(); //Лист студентов        

        string surname;
        string name;
        string adress;
        string phoneNumber;
        DateTime birthday;
        string photoUrl;

        public ClassStudents(string surname, string name, string adress, string phoneNumber, DateTime birthday, string photoUrl)
        {
            this.surname = surname;
            this.name = name;
            this.adress = adress;
            this.phoneNumber = phoneNumber;
            this.birthday = birthday;
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

        static public void SaveToJsonFile() //Сериализация
        {
            string jsonString = JsonSerializer.Serialize(ListStudents);
            File.WriteAllText("students.json", jsonString);
        }
        static public void OpenJsonFile(string filename) //Десериализация
        {
            if (File.Exists(filename))
            {
                string jsonString = File.ReadAllText(filename);
                ListStudents = JsonSerializer.Deserialize<List<ClassStudents>>(jsonString);
            }
        }

        static public int CountStudents()
        {
            return ListStudents.Count;
        }
        static public void ClearStudents()
        {
            ListStudents.Clear();
        }
        static public void InsertStudent(int index, ClassStudents student)
        {
            ListStudents.Insert(index, student);
        }
        public string ToString()
        {
            return Surname + ";" + Name + ";" + Adress + ";" + PhoneNumber + ";" + Birthday + ";" + PhotoUrl;
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
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ошибка сохранения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        static public void LoadFromFile(string filePath)
        {
            ClearStudents();
            try
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] parts = line.Split(';');
                        if (parts.Length >= 6)
                        {
                            DateTime date;
                            if (DateTime.TryParse(parts[4], out date))
                            {
                                date = DateTime.Now;
                                ListStudents.Add(new ClassStudents(parts[0], parts[1], parts[2], parts[3], date, parts[5]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}