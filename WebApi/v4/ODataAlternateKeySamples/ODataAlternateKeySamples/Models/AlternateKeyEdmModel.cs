﻿using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace ODataAlternateKeySamples.Models
{
    public class AlternateKeyEdmModel
    {
        private static IEdmModel _edmModel;
        public static IEdmModel GetEdmModel()
        {
            if (_edmModel != null)
            {
                return _edmModel;
            }

            EdmModel model = new EdmModel();

            // entity type 'Customer' with single alternate keys
            EdmEntityType customer = new EdmEntityType("NS", "Customer");
            customer.AddKeys(customer.AddStructuralProperty("ID", EdmPrimitiveTypeKind.Int32));
            customer.AddStructuralProperty("Name", EdmPrimitiveTypeKind.String);
            var ssn = customer.AddStructuralProperty("SSN", EdmPrimitiveTypeKind.String);
            model.AddAlternateKeyAnnotation(customer, new Dictionary<string, IEdmProperty>
            {
                {"SSN", ssn}
            });
            model.AddElement(customer);

            // entity type 'Order' with multiple alternate keys
            EdmEntityType order = new EdmEntityType("NS", "Order");
            order.AddKeys(order.AddStructuralProperty("OrderId", EdmPrimitiveTypeKind.Int32));
            var orderName = order.AddStructuralProperty("Name", EdmPrimitiveTypeKind.String);
            var orderToken = order.AddStructuralProperty("Token", EdmPrimitiveTypeKind.Guid);
            order.AddStructuralProperty("Amount", EdmPrimitiveTypeKind.Int32);
            model.AddAlternateKeyAnnotation(order, new Dictionary<string, IEdmProperty>
            {
                {"Name", orderName}
            });

            model.AddAlternateKeyAnnotation(order, new Dictionary<string, IEdmProperty>
            {
                {"Token", orderToken}
            });

            model.AddElement(order);

            // entity type 'Person' with composed alternate keys
            EdmEntityType person = new EdmEntityType("NS", "Person");
            person.AddKeys(person.AddStructuralProperty("ID", EdmPrimitiveTypeKind.Int32));
            var country = person.AddStructuralProperty("Country", EdmPrimitiveTypeKind.String);
            var passport = person.AddStructuralProperty("Passport", EdmPrimitiveTypeKind.String);
            model.AddAlternateKeyAnnotation(person, new Dictionary<string, IEdmProperty>
            {
                {"Country", country},
                {"Passport", passport}
            });
            model.AddElement(person);

            // entity sets
            EdmEntityContainer container = new EdmEntityContainer("NS", "Default");
            model.AddElement(container);
            container.AddEntitySet("Customers", customer);
            container.AddEntitySet("Orders", order);
            container.AddEntitySet("People", person);

            return _edmModel = model;
        }
    }
}
