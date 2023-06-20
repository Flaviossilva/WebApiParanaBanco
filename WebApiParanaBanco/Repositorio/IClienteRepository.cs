using Microsoft.EntityFrameworkCore;
using WebApiParanaBanco.Models;

namespace WebApiParanaBanco.Repositorio
{
    public interface IClienteRepository
    {
        Cliente Criar(Cliente cliente);
        Cliente ObterPorTelefone(int DDD,int telefone);
        Cliente ObterPorId(int id);
        List <Cliente> ObterTodos();
        Cliente AtualizarEmail(int id, string novoEmail);

        Telefone AtualizarTelefone(int id, int telefoneId, int novoDDD, int novoNumero);
        void Excluir(string email);


    }

    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteContext _contexto;

        public ClienteRepository(ClienteContext contexto)
        {
            _contexto = contexto;
        }

        public Cliente Criar(Cliente cliente)
        {
            _contexto.Clientes.Add(cliente);
            _contexto.SaveChanges();
            return cliente;
        }




        public List<Cliente> ObterTodos()
        {
            return _contexto.Clientes.Include(c => c.Telefones).ToList();
        }

        public Cliente ObterPorId(int id)
        {
            var cliente = _contexto.Clientes
                .Include(c => c.Telefones)
                .FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                throw new Exception($"Cliente com ID {id} não encontrado.");
            }

            return cliente;
        }

        public Cliente AtualizarEmail(int id, string novoEmail)
        {
            var cliente = _contexto.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null)
            {
                cliente.Email = novoEmail;
                _contexto.SaveChanges();
                return cliente;
            }
            return null;
        }

        public Telefone AtualizarTelefone(int id, int telefoneId, int novoDDD, int novoNumero)
        {
            var cliente = _contexto.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null)
            {
                var telefone = cliente.Telefones.FirstOrDefault(t => t.Id == telefoneId);
                if (telefone != null)
                {

                    telefone.DDD = novoDDD;
                    telefone.Numero = novoNumero;
                    _contexto.SaveChanges();
                    return telefone;
                }
              
            }
            return null;
        }

        public void Excluir(string email)
        {
            var cliente = _contexto.Clientes.FirstOrDefault(c => c.Email == email);
            if (cliente != null)
            {
                _contexto.Clientes.Remove(cliente);
                _contexto.SaveChanges();
            }
        }


        public Cliente ObterPorTelefone(int DDD, int telefone)
        {
            var cliente = _contexto.Clientes
               .Include(c => c.Telefones)
               .FirstOrDefault(c => c.Telefones[0].Numero == telefone);

            if (cliente == null)
            {
                throw new Exception($"Cliente com telefone {telefone} não encontrado.");
            }

            return cliente;
        }
    }

}


