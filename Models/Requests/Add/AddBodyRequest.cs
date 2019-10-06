using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ComputerComplectorWebAPI.Models.Data;

namespace ComputerComplectorWebAPI.Models.Requests.Add
{
    public class AddBodyRequest : AddRequestBase
    {
        private Body _element;
        private List<SqlParameter> _parameters;

        public AddBodyRequest(Body element)
        {
            _element = element;

            _parameters = new List<SqlParameter>
            {
                new SqlParameter("@addition", _element.Additions),

                new SqlParameter("@buildInCharger", _element.BuildInCharger ? 1 : 0)
            };

            if (Validate<int>(_element.ChargerPower, "element.ChargerPower"))
            {
                _parameters.Add(new SqlParameter("@chargerPower", _element.ChargerPower));
            }

            if (Validate<string>(_element.Color, "element.Color"))
            {
                _parameters.Add(new SqlParameter("@color", _element.Color));
            }

            if (Validate<string>(_element.Company, "element.Company"))
            {
                _parameters.Add(new SqlParameter("@company", _element.Company));
            }

            if (Validate<string>(_element.Formfactor, "element.Formfactor"))
            {
                _parameters.Add(new SqlParameter("@formfactor", _element.Formfactor));
            }

            if (Validate<string>(_element.Title, "element.Title"))
            {
                _parameters.Add(new SqlParameter("@title", _element.Title));
            }

            if (Validate<string>(_element.Type, "element.Type"))
            {
                _parameters.Add(new SqlParameter("@type", _element.Type));
            }

            if (Validate<int>(_element.USB2Amount, "element.USB2Ports"))
            {
                _parameters.Add(new SqlParameter("@usb2Amount", _element.USB2Amount));
            }

            if (Validate<int>(_element.USB3Amount, "element.USB3Ports"))
            {
                _parameters.Add(new SqlParameter("@usb3Amount", _element.USB3Amount));
            }

            if (Validate<int>(_element.VideocardMaxLength, "element.VideocardMaxLength"))
            {
                _parameters.Add(new SqlParameter("@vidoecardMaxLength", _element.VideocardMaxLength));
            }
        }

        public string Expression { get; private set; } = "INSERT INTO BODY VALUES (@title, @company, @formfactor, @type, @buildInCharger, @chargerPower, @color, @usb3Amount, @addition, @usb2Amount, @vidoecardMaxLength);";

        public IEnumerable<SqlParameter> Parameters { get => _parameters; private set => _parameters = value.ToList(); }

		public Body Body { get => _element; }
    }
}
