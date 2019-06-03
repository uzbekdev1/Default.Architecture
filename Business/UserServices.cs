﻿using Business.Exceptions;
using Business.Validators;
using Domain.Entities;
using Extensions;
using Repository;
using System;
using System.Reactive.Linq;

namespace Business
{
    public class UserServices
    {
        UserRepository userRepository;
        private ValidatorService validator;
        Crud<long, User> crud;
        public UserServices(UserRepository userRepository, ValidatorService validator, Crud<long, User> crud)
        {
            this.userRepository = userRepository;
            this.validator = validator;
            this.crud = crud;
        }

        public IObservable<User> RegisterAsync(User user)
        {
            return this.validator.CheckAsync(new RegisterUserValidation(), user).SelectMany(validator =>
            {
                return this.userRepository.IsRegistredAsync(user).Select(isRegistred =>
                {
                    if (!isRegistred)
                    {
                        user.Password = user.Password.ToSHA512();
                        return this.crud.Create(user);
                    }
                    else
                    {
                        throw new UserExistentException(user);
                    }
                });
            });
        }
    }
}
