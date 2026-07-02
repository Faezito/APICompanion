using Microsoft.EntityFrameworkCore;

namespace Repositorio
{
    public interface ICRUDGenerico<T> where T : class
    {
        Task<T?> ObterPorIdAsync(int id);
        Task<List<T>> ObterTodosAsync();
        Task AdicionarAsync(T entity);
        void Atualizar(T entity);
        void Adicionar(T entity);
        void Remover(T entity);
        Task SalvarAsync();
    }

    public class CRUDGenerico<T> : ICRUDGenerico<T> where T : class
    {
        protected readonly DbContext _db;
        protected readonly DbSet<T> _dbSet;

        public CRUDGenerico(DbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task<T?> ObterPorIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task<List<T>> ObterTodosAsync()
            => await _dbSet.ToListAsync();

        public async Task AdicionarAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public void Adicionar(T entity)
            => _dbSet.Add(entity);

        public void Atualizar(T entity)
            => _dbSet.Update(entity);

        public void Remover(T entity)
            => _dbSet.Remove(entity);

        public async Task SalvarAsync()
            => await _db.SaveChangesAsync();
    }
}
