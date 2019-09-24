using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;


namespace CustomerRegistration.Entities
{
    public class Customer
    {
            [BsonId()]
            public ObjectId Id { get; set; }

            [BsonIgnoreIfNull]
            public string Cpf { get; set; }

            [BsonIgnoreIfNull]
            public string Name { get; set; }

            [BsonIgnoreIfNull]
            public DateTime BirthDate { get; set; }
    }
}
