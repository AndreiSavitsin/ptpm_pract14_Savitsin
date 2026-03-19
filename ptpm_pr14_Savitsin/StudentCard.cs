using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ptpm_pr9_Savitsin
{
    public partial class StudentCard : Form
    {
        int idStud;
        ClassStudents currentStudent;
        public StudentCard(int id)
        {
            InitializeComponent();
            idStud = id;
            currentStudent = ClassStudents.GetStudents(idStud);
            LoadCurrentStudent();
        }
        private void StudentCard_Load(object sender, EventArgs e)
        {
            LoadCurrentStudent();
        }
        private void LoadCurrentStudent() //Загрузка текущего студента
        {
            try
            {
                studName.Text = currentStudent.Surname + " " + currentStudent.Name;
                textBoxAdress.Text = currentStudent.Adress;
                maskedTextBoxPhone.Text = currentStudent.PhoneNumber;
                dateBorn.Value = currentStudent.Birthday;
                textBoxPhoto.Text = currentStudent.PhotoUrl;
                if (!string.IsNullOrEmpty(currentStudent.PhotoUrl))
                {
                    if (File.Exists($"img/{currentStudent.PhotoUrl}"))
                    {
                        if (pbPhoto.Image != null)
                        {
                            pbPhoto.Image.Dispose();
                        }
                        pbPhoto.Image = Image.FromFile($"img/{currentStudent.PhotoUrl}");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPhoto_Click(object sender, EventArgs e) //Кнопка добавить фото
        {
            openFileDialog1.Filter = "Изображения (*.jpg; *.png; *.bmp; *.gif)|*.jpg; *.png; *.bmp; *.gif|Все файлы (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string sourcePath = openFileDialog1.FileName;
                    string extension = Path.GetExtension(sourcePath);

                    string newFileName = currentStudent.Surname + "_" + currentStudent.Name + extension;
                    string targetPath = $@"img\{newFileName}";

                    Directory.CreateDirectory(Path.GetDirectoryName(targetPath));

                    File.Copy(sourcePath, targetPath, true);
                    textBoxPhoto.Text = newFileName;
                    pbPhoto.Image = Image.FromFile($@"img\{newFileName}");

                    MessageBox.Show("Фотография успешно загружена", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обработке файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveInfo_Click(object sender, EventArgs e) //Кнопка сохранить
        {
            if (textBoxAdress.Text == "" || maskedTextBoxPhone.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBoxPhoto.Text == "")
            {
                MessageBox.Show("Загрузите фото", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string info = studName.Text + ";" + textBoxAdress.Text + ";" + maskedTextBoxPhone.Text + ";" + dateBorn.Value + ";" + textBoxPhoto.Text;
                string[] nameParts = studName.Text.Split(' ');
                currentStudent.Surname = nameParts[0];
                currentStudent.Name = nameParts[1];
                currentStudent.Adress = textBoxAdress.Text;
                currentStudent.PhoneNumber = maskedTextBoxPhone.Text;
                currentStudent.Birthday = dateBorn.Value;
                currentStudent.PhotoUrl = textBoxPhoto.Text;

                MessageBox.Show($"{studName.Text};{textBoxAdress.Text};{maskedTextBoxPhone.Text};{dateBorn.Value};{textBoxPhoto.Text} сохранен");
            }
        }
        private void btnLeft_Click(object sender, EventArgs e) //Кнопка влево
        {
            if (idStud > 0)
            {
                idStud--;
                currentStudent = ClassStudents.GetStudents(idStud);
                LoadCurrentStudent();
            }
            else
            {
                MessageBox.Show("Это первый студент", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRight_Click(object sender, EventArgs e) //Кнопка вправо
        {
            if (idStud < ClassStudents.CountStudents() - 1)
            {
                idStud++;
                currentStudent = ClassStudents.GetStudents(idStud);
                LoadCurrentStudent();
            }
            else
            {
                MessageBox.Show("Это последний студент", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
