
namespace Domain.Commons
{
    public abstract class Entity
    {
        public int Id { get; private set; }
        public bool Deletado { get; private set; }

        public void Deletar() => Deletado = true;
    }
}
