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
        public List<KeyValuePair<int, string>> OriginalList { get; private set; }
        public List<KeyValuePair<int, string>> CloneList { get; private set; }
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
