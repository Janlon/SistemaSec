namespace Generics
{

    public struct Cnpj
    {
        private readonly string _value;
        public Cnpj(string value) { _value = value; }
        public int CalculaNumeroDeDigitos()
        {
            if (_value == null) { return 0; }
            var result = 0;
            for (var i = 0; i < _value.Length; i++)
            { if (char.IsDigit(_value[i])) { result++; } }
            return result;
        }
        public bool VerficarSeTodosOsDigitosSaoIdenticos()
        {
            var previous = -1;
            for (var i = 0; i < _value.Length; i++)
            {
                if (char.IsDigit(_value[i]))
                {
                    var digito = _value[i] - '0';
                    if (previous == -1)
                        previous = digito;
                    else
                    {
                        if (previous != digito)
                            return false;
                    }
                }
            }
            return true;
        }
        public int ObterDigito(int posicao)
        {
            int count = 0;
            for (int i = 0; i < _value.Length; i++)
            {
                if (char.IsDigit(_value[i]))
                {
                    if (count == posicao)
                        return _value[i] - '0';
                    count++;
                }
            }
            return 0;
        }
        public static implicit operator Cnpj(string value) => new Cnpj(value);
        public override string ToString() => _value;
    }


    public struct Cpf
    {
        private readonly string _value;
        public readonly bool EhValido;
        internal Cpf(string value)
        {
            _value = value;
            if (value == null)
            {
                EhValido = false;
                return;
            }
            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;
            var dv1 = 0;
            var dv2 = 0;
            bool digitosIdenticos = true;
            var ultimoDigito = -1;
            foreach (var c in value)
            {
                if (char.IsDigit(c))
                {
                    var digito = c - '0';
                    if (posicao != 0 && ultimoDigito != digito)
                        digitosIdenticos = false;
                    ultimoDigito = digito;
                    if (posicao < 9)
                    {
                        totalDigito1 += digito * (10 - posicao);
                        totalDigito2 += digito * (11 - posicao);
                    }
                    else if (posicao == 9)
                        dv1 = digito;
                    else if (posicao == 10)
                        dv2 = digito;
                    posicao++;
                }
            }
            if (posicao > 11)
            {
                EhValido = false;
                return;
            }
            if (digitosIdenticos)
            {
                EhValido = false;
                return;
            }
            var digito1 = totalDigito1 % 11;
            digito1 = digito1 < 2
                ? 0
                : 11 - digito1;
            if (dv1 != digito1)
            {
                EhValido = false;
                return;
            }
            totalDigito2 += digito1 * 2;
            var digito2 = totalDigito2 % 11;
            digito2 = digito2 < 2
                ? 0
                : 11 - digito2;
            EhValido = dv2 == digito2;
        }
        public static implicit operator Cpf(string value)
            => new Cpf(value);
        public override string ToString() => _value;
    }
}
