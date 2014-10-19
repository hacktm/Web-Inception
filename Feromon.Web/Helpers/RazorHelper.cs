using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer.Repositories;
using Feromon.Web.Models;

namespace Feromon.Web.Helpers
{
    public class RazorHelper
    {
        private static AspNetUserRepository _aspNetUserRepository = null;
        private static PaymentRepository _paymentRepository = null;
        public static CondomPartialModel BuildModel_Gamification_CondomPartial(string aspNetUserName)
        {
            _aspNetUserRepository = new AspNetUserRepository();
            _paymentRepository = new PaymentRepository();
            var currentUser = _aspNetUserRepository.FindAspNetUser(aspNetUserName);
            if (currentUser == null) return new CondomPartialModel(){ Count = 0 };
            var model = new CondomPartialModel {Count = _paymentRepository.FindPaymentCount(currentUser.Id, "condom")};
            return model;
        }

        public static ArrowPartialModel BuildModel_Gamification_ArrowPartial(string aspNetUserName)
        {
            _aspNetUserRepository = new AspNetUserRepository();
            _paymentRepository = new PaymentRepository();
            var currentUser = _aspNetUserRepository.FindAspNetUser(aspNetUserName);
            if(currentUser == null) return  new ArrowPartialModel(){Count = 0};
            var model = new ArrowPartialModel { Count = _paymentRepository.FindPaymentCount(currentUser.Id, "arrow") };
            return model;
        }
    }
}