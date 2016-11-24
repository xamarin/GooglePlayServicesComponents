using System;
using System.Linq;
using System.Collections.Generic;

//namespace Android.Gms.Plus.Model.Moments
//{
//    public partial class MomentBuffer : IEnumerable<IMoment>
//    {
//        public IEnumerator<IMoment> GetEnumerator()
//        {
//            return this.ToEnumerable<IMoment> ().GetEnumerator();
//        }

//        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//        {
//            return this.GetEnumerator();
//        }
//    }
//}

namespace Android.Gms.Plus.Model.People
{
    public partial class PersonBuffer : IEnumerable<IPerson>
    {
        public IEnumerator<IPerson> GetEnumerator()
        {
            return this.ToEnumerable<IPerson> ().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

