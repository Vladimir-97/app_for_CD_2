using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace app_for_CD
{
    public partial class Add : Form
    {
        string doc_num_, ser_num_, date_agg_, state_, kzl_;
        OracleConnection con = null;


        public Add()
        {
            InitializeComponent();
            this.SetConnection();
            label20.Visible = false;
        }
        public Add(string doc_num, string ser_num, string date_agg, string state, string kzl, int but)
        {
            InitializeComponent();
            SetConnection();
            label20.Visible = false;
            this.doc_num_ = doc_num;
            this.ser_num_ = ser_num;
            this.date_agg_ = date_agg;
            this.state_ = state; 
            this.kzl_ = kzl;
            comboBox4.MaxLength = 12;
            dateTimePicker3.Visible = false;
            label19.Visible = false;
            if (but == 1)
            {
               // SetConnection();
                //button4.Visible = false;
                //button5.Visible = false;
                //button6.Visible = false;
                button1.Visible = false;
                comboBox3.Text = "формируется";
                comboBox3.Enabled = false;
            }
            if (but == 2)
            {
                //  SetConnection();
                button4.Visible = false;
                button5.Visible = false;
                //button6.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                inverse_parse_date("20801231", dateTimePicker3);
                show_values();
            }
            if (but == 3)
            {
                // SetConnection();
                button4.Visible = false;
                button5.Visible = false;
                button3.Visible = false;
                button2.Visible = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                //textBox6.Enabled = false;
                //inverse_parse_date("20801231", dateTimePicker3);

                update_values();
            }
        }
        private void Add_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.SetConnection();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT CRP_CD FROM TBCB_CRP_DOCU_INFO where rownum <=1000" ;


            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
           // List<string[]> data = new List<string[]>();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr[0].ToString() );
            }


        }

        private void check_value()
        {
            if (comboBox1.SelectedItem.ToString() == "Э")
            {
                Name_company.Text = "Эмиссионный договор";
            }
            else if (comboBox1.SelectedItem.ToString() == "Ц")
            {
                Name_company.Text = "Договор об оказании услуг депоненту";
            }
            else if (comboBox1.SelectedItem.ToString() == "ЭГ")
            {
                Name_company.Text = "Договор на оказание услуг на использования сервиса `Электронное голосование`";
            }
            else if (comboBox1.SelectedItem.ToString() == "ИП")
            {
                Name_company.Text = "Договор о корреспондентских отношениях с Инвестиционным Посредником";
            }
            else if (comboBox1.SelectedItem.ToString() == "Х")
            {
                Name_company.Text = "Договор об обслуживании хокимията";
            }
            else if (comboBox1.SelectedItem.ToString() == "ОЦ")
            {
                Name_company.Text = "Договор на оказание услуг по проведению оценки с АО";
            }
            else if (comboBox1.SelectedItem.ToString() == "ИК")
            {
                Name_company.Text = "Корпоративное сопровождение АО";
            }
            else if (comboBox1.SelectedItem.ToString() == "К")
            {
                Name_company.Text = "Договор на оказание консультативных услуг с АО";
            }
            else if (comboBox1.SelectedItem.ToString() == "ИУ")
            {
                Name_company.Text = "Информационные услуги согласно договору";
            }
            else if (comboBox1.SelectedItem.ToString() == "WS")
            {
                Name_company.Text = "Договор на обслуживание веб-сайта с АО";
            }
            else if (comboBox1.SelectedItem.ToString() == "ИФ")
            {
                Name_company.Text = "Трехсторонний Контракт на оказание услуг по ведению счета депо ИФ";
            }
            else if (comboBox1.SelectedItem.ToString() == "КО")
            {
                Name_company.Text = "Дополнительное соглашение к договору";
            }
            else
            {
                Name_company.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            check_value();
            find_contract();
        }

        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            string crp = comboBox4.SelectedItem.ToString();
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = crp;
            cmd.CommandText = "SELECT CRP_NM FROM TBCB_CRP_INFO where CRP_CD = :KZL";
            kzl_ = crp;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox6.Text = dr[0].ToString() ;
            }
            comboBox5.Items.Clear();
            ///////////////////////////////
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.SetConnection();
            OracleCommand cmd = con.CreateCommand();
            if (comboBox4.Text != "")
            {
                cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 13).Value = comboBox4.Text;
                cmd.CommandText = "SELECT DOCU_NO, DOCU_SRES FROM TBCB_CRP_DOCU_INFO where CRP_CD = :KZL AND rownum <=1000  ";


                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                // List<string[]> data = new List<string[]>();
                while (dr.Read())
                {
                    comboBox5.Items.Add(dr[0].ToString()) ; /*+ " " + dr[1].ToString())*/
                }
            }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
      {
            string crp = comboBox4.Text.ToString();
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("KZL", OracleDbType.Varchar2, 12).Value = crp;
            cmd.CommandText = "SELECT CRP_NM FROM TBCB_CRP_INFO where CRP_CD = :KZL";
            kzl_ = crp;
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox6.Text = dr[0].ToString();
            }
            comboBox5.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox5.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox1.SelectedItem = "";
            check_value();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            dateTimePicker5.Value = DateTime.Now;
            label20.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (check_empty_str())
            {
                SetConnection();
                query_insert_crp_info();
                query_insert_docu_info();
                query_insert_tbcb_new();
            }

        }

        /// <summary>
        /// ///////////////////////////////////////////////////2 ЗАПРОСА РАБОТАЮТ!!!!!!!!!!!!!! ///////////////////////////
        /// </summary>
        /// <returns></returns>
        bool check_empty_str()
        {
            if (comboBox4.Text == "" || textBox6.Text == "" || comboBox5.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || Name_company.Text == "" || comboBox3.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Введите все необходимые поля!");
                return false;
            }
            return true;
        }

        void query_insert_crp_info()

        {
            OracleCommand cmd = con.CreateCommand();
            //cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));
            //cmd.CommandText = " Select * from tbcb_crp_info where crp_cd = :KZL ";
            //cmd.CommandType = CommandType.Text;
            //OracleDataReader dr = cmd.ExecuteReader();
            //if (dr .HasRows)
            //{
            //    return;
            //}
            //else
            //{
                insert_value();
           // }

        }

        void insert_value()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));
            cmd.Parameters.Add(new OracleParameter("KZL_NM", textBox6.Text));
            string issu_dd = parse_date(dateTimePicker5.Value.ToString(), 2);
            cmd.Parameters.Add(new OracleParameter("ISSU_DD", issu_dd));
            cmd.Parameters.Add(new OracleParameter("DOC_NUM", comboBox5.Text));
            cmd.CommandText = "insert into tbcb_crp_info(CRP_CD, CRP_TYPE_CD, SHRT_CRP_NM, CRP_NM, crp_cat_cd, crp_stat_cd, crp_issu_dd,DOCU_NO)values(:KZL, 7001, 'hello', :KZL_NM, '3', '1', :ISSU_DD,:DOC_NUM)";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        void query_insert_docu_info()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));
            cmd.Parameters.Add(new OracleParameter("NUM_DOCU", comboBox5.Text));
            cmd.Parameters.Add(new OracleParameter("SER_DOCU", comboBox1.Text));
            string st_date = parse_date(dateTimePicker1.Value.ToString(), 2);
            cmd.Parameters.Add(new OracleParameter("DOCU_ISSU", st_date));
            string end_date = parse_date(dateTimePicker2.Value.ToString(), 2);
            cmd.Parameters.Add(new OracleParameter("EXP_DOCU", end_date));
            cmd.Parameters.Add(new OracleParameter("REM1", textBox7.Text));
            cmd.Parameters.Add(new OracleParameter("REM2", textBox8.Text));
            cmd.Parameters.Add(new OracleParameter("STAT", reverse_check_stat(comboBox3.Text)));
            //string reg_date = parse_date(dateTimePicker5.Value.ToString(), 1);
            //cmd.Parameters.Add(new OracleParameter("REG_DOCU", reg_date));

            cmd.CommandText = "insert into tbcb_crp_docu_info(crp_cd, SEQ, dist_id_type_cd, dist_id, docu_no, docu_sres, docu_issu_dd, docu_exp_dd, remark, remark_2, docu_stat_cd)values(:KZL, '1', '08', '7001', :NUM_DOCU, :SER_DOCU, :DOCU_ISSU, :EXP_DOCU , :REM1, :REM2, :STAT) ";
            cmd.CommandType = CommandType.Text;   ///issu_dd yyyymmdd        reg_docu dd.mm.yyyy
            cmd.ExecuteNonQuery();

        }

        void query_insert_tbcb_new()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));

            var price = textBox3.Text.ToString();
            cmd.Parameters.Add(new OracleParameter("DOCU_PR", price));

            var date = parse_date(dateTimePicker4.Value.ToString(), 2);
            cmd.Parameters.Add(new OracleParameter("REC", date));
            string docu_num = comboBox5.Text.ToString();
            cmd.Parameters.Add(new OracleParameter("DOCU_NO", docu_num));

            cmd.Parameters.Add(new OracleParameter("ARCHEE", textBox2.Text));
            cmd.Parameters.Add(new OracleParameter("DESCR", textBox4.Text));
            var tmp_int = fun_ischis(comboBox2.Text);
            cmd.Parameters.Add(new OracleParameter("ISCHIS", tmp_int));
            cmd.Parameters.Add(new OracleParameter("PAR_ISCHIS", comboBox2.Text));
            cmd.Parameters.Add(new OracleParameter("CURRENCY", comboBox6.Text));
            cmd.Parameters.Add(new OracleParameter("BLOCK", "20801231"));


            cmd.CommandText = "insert into new_tbcb (crp_cd,docu_price,get_dd,docu_no, archv, descrpt, estm_cd, estm_nm, currency, block_date)  values(:KZL,:DOCU_PR, :REC, :DOCU_NO, :ARCHEE, :DESCR, :ISCHIS, :PAR_ISCHIS, :CURRENCY, :BLOCK)";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            if (cmd.ExecuteNonQuery() == 1)
                label20.Visible = true;
            else
                MessageBox.Show("Не получилось добавить");
        }



        int fun_ischis(string str)
        {
            if (str == "Сумма")
                return 1;
            return 2;
        }
        string parse_date(string str, int flag = 0)
        {
            string tmp_str = "" ;
            if (flag == 1)
            {
                tmp_str += str[3];
                tmp_str += str[4];
                tmp_str += ".";
                tmp_str += str[6];
                tmp_str += str[7];
                tmp_str += ".";
                //tmp_str += "20";
                // tmp_str += str[9];
                // tmp_str += str[10];
                tmp_str += str[9];
                tmp_str += str[10];


                return tmp_str;
            }
            if (flag == 2)
            {
                tmp_str += "20";
                tmp_str += str[9];
                tmp_str += str[10];
                tmp_str += str[6];
                tmp_str += str[7];
                tmp_str += str[3];
                tmp_str += str[4];
                return tmp_str;
            }
            tmp_str += " ";
            tmp_str += str[12];
            tmp_str += str[13];
            tmp_str += ":";
            tmp_str += str[15];
            tmp_str += str[16];
            tmp_str += ":";
            tmp_str += "00";
            return tmp_str;
        }
        void inverse_parse_date(string str, DateTimePicker dateTimePicker_tmp)
        {
            string tmp_yy = str[0].ToString() + str[1].ToString() + str[2].ToString() + str[3].ToString();
            int yy = Int16.Parse(tmp_yy);
            string tmp_mm = str[4].ToString() + str[5].ToString();
            int mm = Int16.Parse(tmp_mm);
            string tmp_dd = str[6].ToString() + str[7].ToString();
            int dd = Int16.Parse(tmp_dd);
            int x = Convert.ToInt32(yy);
            int y = Convert.ToInt32(mm);
            int z = Convert.ToInt32(dd);

            dateTimePicker_tmp.Value = new DateTime(x, y, z);

        }
        void show_values()
        {
            has_values();
            
        }
        void has_values()
        {
            comboBox4.Text = kzl_;
            comboBox5.Text = doc_num_;
            comboBox1.Text = ser_num_;
            show_from_new_tbcb();
            show_from_crp_docu_info();
            show_from_crp_info();

            textBox3.Enabled = false;
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            comboBox2.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;

            comboBox4.Enabled = false;
            comboBox1.Enabled = false;
            comboBox6.Enabled = false;
            textBox6.Enabled = false;
            Name_company.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker4.Enabled = false;
            dateTimePicker5.Enabled = false;
         //   textBox12.Enabled = false;
            comboBox3.Enabled = false;
            comboBox5.Enabled = false;
        }
        void show_from_new_tbcb()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", kzl_));
            cmd.Parameters.Add(new OracleParameter("DOCU_NO", doc_num_));
            comboBox5.Text = doc_num_;
            cmd.CommandText = "Select * from new_tbcb where crp_cd = :KZL AND docu_no = :DOCU_NO";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    textBox3.Text = dr[1].ToString();
                    inverse_parse_date(dr[2].ToString(), dateTimePicker4);
                    textBox2.Text = check_null(dr[4].ToString());
                    textBox4.Text = check_null(dr[5].ToString());
                    comboBox2.Text = check_null(dr[7].ToString());
                    comboBox6.Text = check_null(dr[8].ToString());
                    inverse_parse_date(dr[9].ToString(), dateTimePicker3);

                }
            }

        }
        void show_from_crp_docu_info()
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", kzl_));
            cmd.Parameters.Add(new OracleParameter("DOCU_NO", doc_num_));
            cmd.Parameters.Add(new OracleParameter("DOC_SER", ser_num_ ));

            cmd.CommandText = "Select remark, remark_2, docu_issu_dd, docu_exp_dd from tbcb_crp_docu_info where crp_cd = :KZL AND docu_no = :DOCU_NO and DOCU_SRES = :DOC_SER";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    textBox7.Text = check_null(dr[0].ToString());
                    textBox8.Text = check_null(dr[1].ToString());
                    inverse_parse_date(dr[2].ToString(), dateTimePicker1);
                    inverse_parse_date(dr[3].ToString(), dateTimePicker2);
                }
            }

        }
        void show_from_crp_info() 
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.Parameters.Add(new OracleParameter("KZL", kzl_));

            cmd.CommandText = "Select crp_stat_cd,crp_issu_dd from tbcb_crp_info where crp_cd = :KZL";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string tmp_str = dr[0].ToString();
                    comboBox3.Text = check_stat(dr[0].ToString());
                    if (dr[0].ToString() == "4")
                    {
                        dateTimePicker3.Visible = true;
                        label19.Visible = true;
                    }
                    else
                    {
                        dateTimePicker3.Visible = false;
                        label19.Visible = false;
                    }
                    inverse_parse_date(dr[1].ToString(), dateTimePicker5);
                }
            }
        }
        void update_values()
        {
            this.SetConnection();
            comboBox4.Text = kzl_;
            comboBox5.Text = doc_num_;
            comboBox1.Text = ser_num_;
            show_from_new_tbcb();
            show_from_crp_info();
            show_from_crp_docu_info();
            comboBox5.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SetConnection();
            update_crp_info();
            update_crp_docu_info();
            update_new_tbcb();
        }
        void update_crp_info()
        {
            OracleCommand cmd = con.CreateCommand();

            cmd.Parameters.Add(new OracleParameter("CRP_CAT", "1"));

            string str = reverse_check_stat(comboBox3.Text);
            cmd.Parameters.Add(new OracleParameter("CRP_STAT", str));
            cmd.Parameters.Add(new OracleParameter("DOC_NUM", comboBox5.Text));

            string issu_dd = parse_date(dateTimePicker5.Value.ToString(), 2);
            cmd.Parameters.Add(new OracleParameter("ISSU_DD", issu_dd));
            cmd.Parameters.Add(new OracleParameter("KZL", kzl_));
            cmd.CommandText = "update tbcb_crp_info set crp_cat_cd = :CRP_CAT, crp_stat_cd= :CRP_STAT, DOCU_NO = :DOC_NUM, crp_issu_dd = :ISSU_DD where CRP_CD = :KZL";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        void update_crp_docu_info()
        {
            OracleCommand cmd = con.CreateCommand();

            cmd.Parameters.Add(new OracleParameter("SER_DOCU", comboBox1.Text));
            string st_date = parse_date(dateTimePicker1.Value.ToString(), 2);
            cmd.Parameters.Add(new OracleParameter("DOCU_ISSU", st_date));
            string end_date = parse_date(dateTimePicker2.Value.ToString(), 2);
            cmd.Parameters.Add(new OracleParameter("EXP_DOCU", end_date));
            cmd.Parameters.Add(new OracleParameter("REM1", textBox7.Text));
            cmd.Parameters.Add(new OracleParameter("REM2", textBox8.Text));
            cmd.Parameters.Add(new OracleParameter("STAT", reverse_check_stat(comboBox3.Text)));

            //string reg_date = parse_date(dateTimePicker5.Value.ToString(), 1);
            //cmd.Parameters.Add(new OracleParameter("REG_DOCU", reg_date));

            cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));
            cmd.Parameters.Add(new OracleParameter("NUM_DOCU", comboBox5.Text));

            cmd.CommandText = "update tbcb_crp_docu_info set docu_sres = :SER_DOCU, docu_issu_dd=:DOCU_ISSU, docu_exp_dd = :EXP_DOCU, remark = :REM1, remark_2= :REM2, DOCU_STAT_CD = :STAT where crp_cd = :KZL and DOCU_NO = :NUM_DOCU";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        void update_new_tbcb()
        {
                OracleCommand cmd = con.CreateCommand();

                var price = textBox3.Text.ToString();
                cmd.Parameters.Add(new OracleParameter("DOCU_PR", price));
                var date = parse_date(dateTimePicker4.Value.ToString(), 2);
                cmd.Parameters.Add(new OracleParameter("REC", date));
                cmd.Parameters.Add(new OracleParameter("ARCHEE", textBox2.Text));
                cmd.Parameters.Add(new OracleParameter("DESCR", textBox4.Text));
                var tmp_int = fun_ischis(comboBox2.Text);
                cmd.Parameters.Add(new OracleParameter("ISCHIS", tmp_int));
                cmd.Parameters.Add(new OracleParameter("PAR_ISCHIS", comboBox2.Text));
                cmd.Parameters.Add(new OracleParameter("CURRENCY", comboBox6.Text));
                cmd.Parameters.Add(new OracleParameter("BLOCK", parse_date(dateTimePicker3.Value.ToString(), 2)));

                cmd.Parameters.Add(new OracleParameter("KZL", comboBox4.Text));
                string docu_num = comboBox5.Text.ToString();
                cmd.Parameters.Add(new OracleParameter("DOCU_NO", docu_num));



            cmd.CommandText = "update new_tbcb set docu_price = :DOCU_PR , get_dd = :REC, archv = :ARCHEE, descrpt = :DESCR, estm_cd =:ISCHIS, estm_nm = :PAR_ISCHIS, currency = :CURRENCY, block_date = :BLOCK where crp_cd = :KZL and docu_no = :DOCU_NO";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                if (cmd.ExecuteNonQuery() > 0)
                    label20.Visible = true;
                else
                    MessageBox.Show("Не получилось изменить");

        }

        string check_stat(string tmp_str)
        {
            if (tmp_str == "1")
            {
                return "действующий документ";
            }
            if (tmp_str == "2")
            {
                return "недействительный документ";
            }
            if (tmp_str == "3")
            {
                return "формируется";
            }
            if (tmp_str == "4")
            {

                return "блокированный документ";
            }
            if (tmp_str == "5")
            {
               return "нераспознанный документ";
            }
            return "";
        }
        string reverse_check_stat(string tmp_str)
        {
            if (tmp_str == "действующий документ")
            {
                return "1";
            }
            if (tmp_str == "недействительный документ")
            {
                return "2";
            }
            if (tmp_str == "формируется")
            {
                return "3";
            }
            if (tmp_str == "блокированный документ")
            {
                return "4";
            }
            if (tmp_str == "нераспознанный документ")
            {
                return "5";
            }
            return "";
        }

        private void comboBox4_Enter(object sender, EventArgs e)
        {
            this.SetConnection();
        }

        private void comboBox4_Leave(object sender, EventArgs e)
        {
            this.CloseConnection();
        }
        

        string check_null(string str)
        {
            if (str.Length == 0)
                return "";
            else return str;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reverse_check_stat(comboBox3.Text) == "4")
            {
                dateTimePicker3.Visible = true;
                label19.Visible = true;
            }
            else
            {
                dateTimePicker3.Visible = false;
                label19.Visible = false;
            }
        }

        bool is_empty_str(string str)
        {
            if (str == "")
                return true;
            else return false;
        }
        void find_contract()
        {
            SetConnection();
            OracleCommand cmd = con.CreateCommand();
           // cmd.Parameters.Add(new OracleParameter("NUM_DOCU", comboBox5.Text));
            cmd.Parameters.Add(new OracleParameter("SER_DOCU", comboBox1.Text));
            cmd.CommandText = "select max(DOCU_NO) from tbcb_crp_docu_info where docu_sres = :SER_DOCU ";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                long tmp;
                if (!is_empty_str(dr[0].ToString() ) )
                {
                    tmp = Int64.Parse(dr[0].ToString());
                   
                }
                else
                {
                    tmp = 0;
                }
                tmp += 1;
                comboBox5.Text = tmp.ToString();
            }

        }
        private void SetConnection()
        {
            string ConnectionString = "USER ID=GGUZDR_APP;PASSWORD=gguzdr_app;DATA SOURCE=10.1.50.12:1521/GDBDRCT1";
            con = new OracleConnection(ConnectionString);
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, e.Message);

                MessageBox.Show(errorMessage, "Error");
            }
        }
        private void CloseConnection()
        {
            con.Close();
        }
    }
}
