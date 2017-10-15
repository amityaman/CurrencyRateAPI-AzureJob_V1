using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zebpay.Common.Utilies.Mapper
{
    public interface IMapper<in TSource, TTarget>
    {
        /// <summary>
        /// Maps the source to a new instance of the target type.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>New instance of the target typed mapped from the source type</returns>
        TTarget MapToNew(TSource source);

        /// <summary>
        /// Maps the source to an existing instance of the target type.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">instance of the target type, 
        /// which will be populated by the provided instance of the source type</param>
        void MapToExisting(TSource source, TTarget target);
    }

   
}
