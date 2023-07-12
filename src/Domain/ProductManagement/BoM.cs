using Domain.SharedKernel.Base;

namespace Domain.ProductManagement
{
    public class BoM : Entity<uint>
    {
        private readonly List<BoMRevision> _boMRevisions = new();

        public string Code { get; }
        
        public virtual IReadOnlyCollection<BoMRevision> BoMRevisions => _boMRevisions.AsReadOnly();
        
        //required EF
        //protected BoM() {}
        
        public BoM()
        {
            Code = Id.ToString("BOM000000#");
        }
    }
}
