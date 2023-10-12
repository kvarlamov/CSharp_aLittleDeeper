using System;
using System.Collections.Generic;

namespace p11_DesighnPatterns.Behavioral
{
    
    // with visitor we able to add behavior without modify entities and be able to add extra behaviours 
    
    public class VisitorEx
    {
        public static void Test()
        {
            NoticeBox noticeBox = new NoticeBox(new List<BaseEntity>
            {
                new Client("Jack", "Daniels"),
                new Company("Jameson Inc.")
            });
            
            noticeBox.Notice(new EmailNoticeNoticeVisitor());
            noticeBox.Notice(new TelegramNoticeNoticeVisitor());
        }
        
        private interface INoticeVisitor
        {
            void NoticeClient(Client client);
            void NoticeCompany(Company company);
        }
        
        private class EmailNoticeNoticeVisitor : INoticeVisitor
        {
            public void NoticeClient(Client client)
            {
                Console.WriteLine($"notice client {client.FirstName} {client.LastName} by email");
            }

            public void NoticeCompany(Company company)
            {
                Console.WriteLine($"notice company {company.CompanyName} by email");
            }
        }
        
        private class TelegramNoticeNoticeVisitor : INoticeVisitor
        {
            public void NoticeClient(Client client)
            {
                Console.WriteLine($"notice client {client.FirstName} {client.LastName} by telegram");
            }

            public void NoticeCompany(Company company)
            {
                Console.WriteLine($"notice company {company.CompanyName} by telegram");
            }
        }

        #region Entities

        private abstract class BaseEntity
        {
            public abstract void Accept(INoticeVisitor noticeVisitor);
        }

        private class Client : BaseEntity
        {
            public string FirstName { get; }

            public string LastName { get; }

            public Client(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }
            
            // this technique called Double Dispatch
            public override void Accept(INoticeVisitor noticeVisitor) => noticeVisitor.NoticeClient(this);
        }
        
        private class Company : BaseEntity
        {
            public string CompanyName { get; }

            public Company(string companyName)
            {
                CompanyName = companyName;
            }
            public override void Accept(INoticeVisitor noticeVisitor) => noticeVisitor.NoticeCompany(this);
        }
        
        private class NoticeBox
        {
            private readonly List<BaseEntity> _entities = new List<BaseEntity>();

            public NoticeBox(BaseEntity entity)
            {
                _entities.Add(entity);
            }

            public NoticeBox(IEnumerable<BaseEntity> entities)
            {
                _entities.AddRange(entities);
            }

            public void Notice(INoticeVisitor noticeVisitor)
            {
                //Double Dispatch technique - each entity called proper method
                _entities.ForEach(e => e.Accept(noticeVisitor));
            }
        }

        #endregion

    }
}