
using System.Net.Http.Headers;

namespace TikTakToe.Models
{
    public class Parser
    {
        public static Field FromFieldDtoToField(FieldDto fieldDto)
        {
            var positionsString = fieldDto.Positions.Split(',');
            var fieldPositions = new Marks[3, 3];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    fieldPositions[row,col] = (Marks) int.Parse(positionsString[row*3+col]);
                }
            }
            return new Field
            {
                Id = fieldDto.Id,
                Players = fieldDto.Players,
                Status = fieldDto.Status,
                StatusId = fieldDto.StatusId,
                Turn = fieldDto.Turn,
                TurnId = fieldDto.TurnId,
                Positions = fieldPositions
            };
        }
        public static FieldDto FromFieldToFieldDto(Field field)
        {
            var positionString = string.Empty;
            foreach (var position in field.Positions)
            {
                positionString += $"{(int)position},";
            }
            positionString = positionString.Remove(positionString.Length - 1);
            return new FieldDto
            {
                Id = field.Id,
                Players = field.Players,
                Status = field.Status,
                StatusId = field.StatusId,
                Turn = field.Turn,
                TurnId = field.TurnId,
                Positions = positionString
            };
        }
    }
}
