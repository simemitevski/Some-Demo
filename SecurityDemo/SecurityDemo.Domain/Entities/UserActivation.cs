using System;

namespace SecurityDemo.Domain.Entities
{
    public class UserActivation
    {
        public UserActivation()
        {

        }

        public UserActivation(int userId, Guid activationCode)
        {
            this.UserId = userId;
            this.ActivationCode = activationCode;
            this.ExpiresOn = DateTime.Now;
            this.IsActive = true;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid ActivationCode { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }

        void SetStatus (bool status)
        {
            this.IsActive = status;
        }
    }
}
