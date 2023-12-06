using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.Business.Dtos
{
   
    public class GetAllWithCloneResponse
    {
        public GetAllWithCloneResponse(IEnumerable<KeyValuePair<int,string>> OrginalList)
        {
            this.OriginalList = OrginalList.ToList();
            this.CloneList = MixedAndClone(OriginalList);
        }
        /// <summary>
        /// buslocations list
        /// </summary>
        public List<KeyValuePair<int, string>> OriginalList { get; private set; }
        /// <summary>
        /// clone and mixed list
        /// </summary>
        public List<KeyValuePair<int, string>> CloneList { get; private set; }

        /// <summary>
        /// This method shuffles the 'buslocations' list into a second list and does not bring data that matches index-wise with each other.
        /// </summary>
        private List<KeyValuePair<int,string>> MixedAndClone(List<KeyValuePair<int, string>> originalList)
        {
            var clonedList = originalList.Select(item => item).ToList();
            var random = new Random();
            clonedList = clonedList.OrderBy(item => random.Next()).ToList();
            while (Enumerable.Range(0, originalList.Count).Any(i => originalList[i].Equals(clonedList[i])))
            {
                clonedList = clonedList.OrderBy(item => random.Next()).ToList();
            }
            return clonedList;
        }
    }
}
