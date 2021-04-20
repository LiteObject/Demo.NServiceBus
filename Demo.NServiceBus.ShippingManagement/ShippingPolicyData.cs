using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Demo.NServiceBus.ShippingManagement
{
    /* SAGA STATE:
     *
     * Sagas store their state in a class that inherits from ContainSagaData which automatically
     * gives it a few properties (including an Id) required by NServiceBus. All the saga's data
     * is represented as properties on the saga data class.
     *
     * We could implement IContainSagaData instead and create these required properties ourselves,
     * but it's a lot easier to use the ContainSagaData convenience class.
     */

    public class ShippingPolicyData : ContainSagaData
    {
        /// <summary>
        /// We do not have to worry about is filling in OrderId in the saga data. 
        /// </summary>
        public string OrderId { get; set; }
        public bool OrderCreated { get; set; }
        public bool BillingRecordCreated { get; set; }
    }
}
