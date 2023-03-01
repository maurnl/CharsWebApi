using App.Application.Dtos;
using App.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services.Abstractions
{
    public interface IBattleService
    {
        BattleResultReadDto GenerateBattle(int idCharacterOne, int idCharacterTwo);
    }
}
