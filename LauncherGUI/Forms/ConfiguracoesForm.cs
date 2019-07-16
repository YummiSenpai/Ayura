﻿using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bot.Forms
{
    public partial class ConfiguracoesForm : Form
    {

        public ConfiguracoesForm()
        {
            InitializeComponent();
        }

        private void BtPicInicializarSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DiaConfig diaConfig = new DiaConfig(txBotToken.Text, txBotPrefix.Text, Convert.ToUInt64(txBotIDDono.Text));
                DBConfig dBConfig = new DBConfig(txDBIP.Text, txDBDatabase.Text, txDBLogin.Text, txDBSenha.Text);
                ApiConfig apiConfig = new ApiConfig(txWeebAPIToken.Text);

                DBDAO dao = new DBDAO();
                dao.AdicionarAtualizar(apiConfig, dBConfig, diaConfig);
                MessageBox.Show("Dados atualizados com sucesso", "Kurosawa Dia - Tarefa Completa Senpai :D", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("O campo ID dono so pode conter numeros!!", "Kurosawa Dia - Problemas com os dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("O valor do campo ID dono excedeu o limite de dados, verifique se digitou corretamente!", "Kurosawa Dia - Problemas com os dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Ops, isso é muito embaraçoso.... Espero que você entenda e corrija XD\n\n" + erro.Message, "Kurosawa Dia - Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfiguracoesForm_Load(object sender, EventArgs e)
        {
            DBDAO dao = new DBDAO();
            var retorno = dao.PegarDadosBot();

            if (retorno.Item1 == 3)
            {
                txBotToken.Text = retorno.Item4.token;
                txBotPrefix.Text = retorno.Item4.prefix;
                txBotIDDono.Text = retorno.Item4.idDono.ToString();

                txDBIP.Text = retorno.Item3.ip;
                txDBDatabase.Text = retorno.Item3.database;
                txDBLogin.Text = retorno.Item3.login;
                txDBSenha.Text = retorno.Item3.senha;

                txWeebAPIToken.Text = retorno.Item2.WeebToken;
            }


            StatusDAO sdao = new StatusDAO();
            var retorno2 = sdao.CarregarStatus();

            if (retorno2.Item1)
            {
                foreach (Status status in retorno2.Item2)
                {
                    dtStatusEdit.Rows.Add(status.status_jogo, status.status_url, status.status_tipo, (int)status.status_tipo);
                }
            }
        }


        private void BtStatusAdicionar_Click(object sender, EventArgs e)
        {
            if (txStatusStatus.Text != null && cbStatusTipo.SelectedIndex >= 0)
            {
                dtStatusEdit.Rows.Add(txStatusStatus.Text, txUrl.Text, cbStatusTipo.Text, cbStatusTipo.SelectedIndex);
                txStatusStatus.Clear();
                txUrl.Clear();
                txStatusStatus.Focus();
                cbStatusTipo.SelectedIndex = -1;
            }
        }

        private void DtStatusEdit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            if (dg.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                DataGridViewRow row = dtStatusEdit.Rows[e.RowIndex];
                dtStatusEdit.Rows.Remove(row);
            }
        }

        private void BtStatusRedefinir_Click(object sender, EventArgs e)
        {
            dtStatusEdit.Rows.Clear();
            new StatusDAO().RemoverTabela();
        }

        private void BtStatusSalvar_Click(object sender, EventArgs e)
        {
            if (dtStatusEdit.Rows.Count > 0)
            {
                List<Status> statuses = new List<Status>();
                for (int i = 0; i < dtStatusEdit.RowCount; i++)
                {
                    Status temp = new Status(dtStatusEdit.Rows[i].Cells[0].Value.ToString(), (Status.TiposDeStatus)Convert.ToInt32(dtStatusEdit.Rows[i].Cells[3].Value), dtStatusEdit.Rows[i].Cells[1].Value.ToString());
                    statuses.Add(temp);
                }

                StatusDAO dao = new StatusDAO();
                dao.AdicionarAtualizarStatus(statuses);
                MessageBox.Show("Dados atualizados com sucesso", "Kurosawa Dia - Tarefa Completa Senpai :D", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtIdiomasSalvar_Click(object sender, EventArgs e)
        {
            if(cbIdiomasIdioma.SelectedIndex > 0 && txIdiomasIdentificador.Text != "" && txIdiomasTexto.Text != "")
            {
                Linguagens linguagem = new Linguagens((Linguagens.Idiomas)cbIdiomasIdioma.SelectedIndex, txIdiomasIdentificador.Text, txIdiomasTexto.Text);
                LinguagensDAO dao = new LinguagensDAO();
                dao.Adicionar(linguagem);
                MessageBox.Show("Dados salvos com sucesso", "Kurosawa Dia - Tarefa Completa Senpai :D", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //pitas ponha as msg de erro aki <3
        }
    }
}
