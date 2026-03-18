using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ptpm_pr9_Savitsin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void UpdateListBox(List<string> list) //ЛистБокс
        {
            listBox1.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                string[] parts = list[i].Split(';');
                if (parts.Length >= 2)
                {
                    listBox1.Items.Add($"{parts[0]} {parts[1]}");
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e) //Кнопка Добавить
        {
            error.Text = "";
            string name = $"{textBoxName.Text}";
            string surname = $"{textBoxSurname.Text}";
            string fullName = surname + ";" + name;

            if (name == "" || surname == "")
            {
                error.Text = "Нельзя оставлять поля пустыми";
            }
            else
            {
                if (ClassStudents.GetList.Contains(fullName) == true)
                {
                    error.Text = "Такое имя и фамилия уже есть";
                }
                else
                {
                    ClassStudents.ListStudents.Add(surname + ";" + name);
                    UpdateListBox(ClassStudents.ListStudents);
                }
            }
        }
        private void btnChange_Click(object sender, EventArgs e) //Кнопка Изменить
        {
            error.Text = "";
            if (listBox1.SelectedIndex != -1)
            {
                string name = $"{textBoxName.Text}";
                string surname = $"{textBoxSurname.Text}";

                if (name == "" || surname == "")
                {
                    error.Text = "Нельзя оставлять поля пустыми";
                }
                else
                {
                    string currentStudent = ClassStudents.ListStudents[listBox1.SelectedIndex];
                    string[] parts = currentStudent.Split(';');
                    string newStudent = "";

                    if (parts.Length >= 6)
                    {
                        newStudent = $"{surname};{name};{parts[2]};{parts[3]};{parts[4]};{parts[5]}";
                    }
                    else if (parts.Length >= 2)
                    {
                        newStudent = $"{surname};{name}";
                    }

                    if (parts[0] == surname && parts[1] == name)
                    {
                        error.Text = "Такое имя и фамилия уже есть";
                    }
                    else
                    {
                        ClassStudents.ListStudents[listBox1.SelectedIndex] = newStudent;
                        UpdateListBox(ClassStudents.ListStudents);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите студента");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e) //Кнопка Удалить
        {
            error.Text = "";
            ClassStudents.RemoveStudent(listBox1.SelectedIndex);
            UpdateListBox(ClassStudents.);
        }
        private void btnInsert_Click(object sender, EventArgs e) //Кнопка Вставить
        {
            error.Text = "";
            string name = $"{textBoxName.Text}";
            string surname = $"{textBoxSurname.Text}";
            string currentStudent = ClassStudents.ListStudents[listBox1.SelectedIndex];
            string[] parts = currentStudent.Split(';');
            if (name == "" || surname == "")
            {
                error.Text = "Нельзя оставлять поля пустыми";
            }
            else
            {
                if (parts[0] == surname && parts[1] == name)
                {
                    error.Text = "Такое имя и фамилия уже есть";
                }
                else
                {
                    for (int i = 0; i < ClassStudents.ListStudents.Count; i++)
                    {
                        if (i == listBox1.SelectedIndex)
                        {
                            ClassStudents.ListStudents.Insert(i + 1, surname + ";" + name);
                        }
                    }
                }
            }
            UpdateListBox(ClassStudents.ListStudents);
        }
        private void btnClear_Click(object sender, EventArgs e) //Кнопка Очистить
        {
            error.Text = "";
            ClassStudents.ClearStudents();
            UpdateListBox(ClassStudents.ListStudents);
        }
        private void btnSort_Click(object sender, EventArgs e) //Кнопка Сортировка
        {
            if (btnSort.Text == "Сортировать")
            {
                btnSort.Text = "Не сортировать";
                listBox1.Sorted = true;
            }
            else if (btnSort.Text == "Не сортировать")
            {
                btnSort.Text = "Сортировать";
                listBox1.Sorted = false;
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) //Лист с именами и фамилиями
        {
        }
        private void btnSave_Click(object sender, EventArgs e) //Кнопка Сохранить в файл
        {
            error.Text = "";
            saveFileDialog1.Filter = "txt файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != string.Empty)
                {
                    using (StreamWriter sw = File.CreateText(saveFileDialog1.FileName))
                    {
                        foreach (var item in ClassStudents.ListStudents)
                        {
                            sw.WriteLine(item);
                        }
                    }
                    textBoxFile.Text = saveFileDialog1.FileName;
                }
            }
            MessageBox.Show($"Сохранено в {Directory.GetCurrentDirectory()}", "Сохранение");
        }
        private void btnLoad_Click(object sender, EventArgs e) //Кнопка Загрузить из файл
        {
            error.Text = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = File.OpenText(openFileDialog1.FileName))
                {
                    ClassStudents.ClearStudents();
                    while (!sr.EndOfStream)
                    {
                        ClassStudents.ListStudents.Add(sr.ReadLine());
                    }
                    MessageBox.Show("Файл загружен");
                }
                UpdateListBox(ClassStudents.ListStudents);
                textBoxFile.Text = saveFileDialog1.FileName;
            }
        }
        private string TrueText(string text) //Проверка правильности текста
        {
            char[] chars1 = text.ToCharArray();
            string new_text = "";
            for (int i = 0; i < text.Length; i++)
            {
                char c = chars1[i];
                if (char.IsLetter(c))
                {
                    new_text += c;
                }
            }
            return $"{new_text}";
        }
        private void textBoxSurname_TextChanged(object sender, EventArgs e) //Текстовое поле для фамилии
        {
            textBoxSurname.Text = TrueText(textBoxSurname.Text);
            textBoxSurname.SelectionStart = textBoxSurname.Text.Length;
        }
        private void textBoxName_TextChanged(object sender, EventArgs e) //Текстовое поле для имени
        {
            textBoxName.Text = TrueText(textBoxName.Text);
            textBoxName.SelectionStart = textBoxName.Text.Length;
        }
        private void btnLeft_Click(object sender, EventArgs e) //Кнопка влево
        {
            error.Text = "";
            for (int i = 0; i < ClassStudents.CountStudents(); i++)
            {
                if (listBox1.SelectedIndex == -1)
                {
                    listBox1.SelectedIndex = 0;
                    error.Text = "Листать влево с этой позиции нельзя";
                }
                else
                {
                    if (i == listBox1.SelectedIndex)
                    {
                        listBox1.SelectedIndex = i - 1;
                    }
                }
            }
        }
        private void btnRight_Click(object sender, EventArgs e) //Кнопка вправо
        {
            error.Text = "";
            for (int i = 0; i < ClassStudents.CountStudents(); i++)
            {
                if (listBox1.SelectedIndex == ClassStudents.CountStudents() - 1)
                {
                    error.Text = "Листать вправо с этой позиции нельзя";
                }
                else
                {
                    if (i == listBox1.SelectedIndex)
                    {
                        listBox1.SelectedIndex = i + 1;
                        break;
                    }
                }
            }
        }

        private void btnChangeInfo_Click(object sender, EventArgs e) //Кнопка редактировать личные данные
        {
            try
            {
                int id = listBox1.SelectedIndex;
                if (id != -1)
                {
                    StudentCard studentsCard = new StudentCard(id);
                    studentsCard.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Выберите пользователя");
                }
            }
            catch
            {
                MessageBox.Show("Элемент не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}