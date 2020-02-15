using System.Collections.Generic;
using Core.Model;
using CoreS.Model;

namespace CoreS.Interface
{
    public interface IElementRepository
    {
        //List<ElementModel> Add();
        List<ElementModelDTO> Add(int element);

        List<ElementModelDTO> Delete(int delete);
        List<ElementModelDTO> Delete(ElementModelDTO element);
        //List<ElementModel> Delete(Guid guid);
        List<ElementModelDTO> DeleteAll();
        
        List<ElementModelDTO> GetAll();
        //ElementModel Get(Guid guid);
        

        List<ElementModelDTO> ReCount();
        List<ElementModelDTO> NewBarrier(BarrierParameter barrierParameter);
    }
}