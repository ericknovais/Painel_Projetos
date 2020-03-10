using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painel_Projetos.DomainModel.Class
{
    public abstract class EntityBase
    {
        public int ID { get; set; }
        protected StringBuilder _msgErro = new StringBuilder();

        public virtual void Validar()
        {
            if (_msgErro.Length > 0)
            {
                throw new Exception(_msgErro.ToString());
            }
        }
    }
}
