using App.Application.Dtos;
using App.Application.Services.Abstractions;
using App.Core.Model;
using App.DataAccess.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class BattleService : IBattleService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Character> _characterRepo;
        private readonly IUserService _userService;

        public BattleService(IMapper mapper,
                             IRepository<Character> characterRepo,
                             IUserService userService)
        {
            _mapper = mapper;
            _characterRepo = characterRepo;
            _userService = userService;
        }

        public BattleResultReadDto GenerateBattle(int idCharacterOne, int idCharacterTwo)
        {
            ThrowIfRequestUserIsNotOwner(idCharacterOne);
            int dice = new Random().Next(10);
            var result = new BattleResult
            {
                Winner = dice == 0 ? _characterRepo.Get(idCharacterOne) : _characterRepo.Get(idCharacterTwo)
            };
            return _mapper.Map<BattleResultReadDto>(result);
        }

        private void ThrowIfRequestUserIsNotOwner(int characterId)
        {
            var requestUsersCharacters = _characterRepo.GetAll().Where(c => c.OwnerId == _userService.GetCurrentUserGuid());
            if(requestUsersCharacters.Any(c => c.Id == characterId))
            {
                throw new ArgumentException(nameof(characterId));
            }
        }
    }
}
