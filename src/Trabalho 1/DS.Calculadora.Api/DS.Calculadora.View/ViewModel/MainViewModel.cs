using System;
using System.Collections.Specialized;
using System.Net;
using System.Windows;
using System.Windows.Input;
using DS.Calculadora.Stub.BO;
using DS.Calculadora.Stub.Commom.Exceptions;
using DS.Calculadora.Stub.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PropertyChanged;

namespace DS.Calculadora.View.ViewModel
{
    [ImplementPropertyChanged]
    public class MainViewModel : ViewModelBase
    {
        private CalculadoraCliente calculadora;

        public MainViewModel()
        {
            InitCommand();
            IsReg = true;
        }

        private void InitCommand()
        {

            OnSoma = new RelayCommand(Soma);
            OnSub = new RelayCommand(Subtracao);
            OnMult = new RelayCommand(Multiplicacao);
            OnDiv = new RelayCommand(Divisao);
            OnRegServer = new RelayCommand(RegServer, () => IsReg);
        }

        public bool IsReg { get; set; }

        private void Divisao()
        {
            try
            {
                ValidaCalculadora();
                ValidaParam();
                Result = calculadora.Divisao(Valor1, Valor2);
            }
            catch (NaoExisteUrlRegistradaException e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButton.OKCancel);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButton.OKCancel);
            }
        }

        private void Multiplicacao()
        {
            try
            {
                ValidaCalculadora();
                ValidaParam();
                Result = calculadora.Multiplicacao(Valor1, Valor2);
            }
            catch (NaoExisteUrlRegistradaException e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButton.OKCancel);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButton.OKCancel);
            }
        }

        private void Subtracao()
        {
            try
            {
                ValidaCalculadora();
                ValidaParam();
                Result = calculadora.Subtracao(Valor1, Valor2);
            }
            catch (NaoExisteUrlRegistradaException e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButton.OKCancel);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButton.OKCancel);
            }
        }

        private void RegServer()
        {
            if (string.IsNullOrEmpty(UrlServer) || string.IsNullOrEmpty(Port))
            {
                MessageBox.Show("Informe o caminho do servidor!!!", "Erro", MessageBoxButton.OKCancel);
                return;
            }
            Url = string.Format("{0}:{1}", UrlServer, Port);
            calculadora = new CalculadoraCliente(Url);
            IsReg = false;
        }

        private void Soma()
        {
            try
            {
                ValidaCalculadora();
                ValidaParam();
                Result = calculadora.Soma(Valor1, Valor2);
            }
            catch (NaoExisteUrlRegistradaException e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButton.OKCancel);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButton.OKCancel);
            }

        }

        private void ValidaParam()
        {
            if (Valor1 == null || Valor2 == null)
                throw new Exception("Valores Invalidos.");
        }

        private void ValidaCalculadora()
        {
            if (calculadora == null)
                throw new NaoExisteUrlRegistradaException("Informe o caminho do servidor!!!");
        }

        public string Url { get; set; }
        public double Result { get; set; }
        public long Valor1 { get; set; }
        public long Valor2 { get; set; }

        public string UrlServer { get; set; }
        public string Port { get; set; }

        public ICommand OnRegServer { get; set; }
        public ICommand OnSoma { get; set; }
        public ICommand OnSub { get; set; }
        public ICommand OnMult { get; set; }
        public ICommand OnDiv { get; set; }
    }
}