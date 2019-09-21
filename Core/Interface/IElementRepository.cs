using System;
using System.Collections.Generic;
using Core.Model;

namespace Core.Interface
{
    public interface IElementRepository
    {
        //List<ElementModel> Add();
        List<ElementModel> Add(int element);

        List<ElementModel> Delete();
        List<ElementModel> Delete(ElementModel element);
        //List<ElementModel> Delete(Guid guid);
        List<ElementModel> DeleteAll();
        
        List<ElementModel> GetAll();
        //ElementModel Get(Guid guid);
        

        List<ElementModel> ReCount();

    }
}