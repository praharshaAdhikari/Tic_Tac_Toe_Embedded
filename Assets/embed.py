class Board:
    def __init__(self, info):
        self.info = info

    def returnBestMove(self):
        def isMovesLeft(b):
            for i in range(9):
                if b[i] == '-':
                    return True
            return False

        def evaluate(b):
            if b[0] == 'X' and b[1] == 'X' and b[2] == 'X':
                return 10
            if b[0] == 'O' and b[1] == 'O' and b[2] == 'O':
                return -10
            if b[3] == 'X' and b[4] == 'X' and b[5] == 'X':
                return 10
            if b[3] == 'O' and b[4] == 'O' and b[5] == 'O':
                return -10
            if b[6] == 'X' and b[7] == 'X' and b[8] == 'X':
                return 10
            if b[6] == 'O' and b[7] == 'O' and b[8] == 'O':
                return -10
            if b[0] == 'X' and b[3] == 'X' and b[6] == 'X':
                return 10
            if b[0] == 'O' and b[3] == 'O' and b[6] == 'O':
                return -10
            if b[1] == 'X' and b[4] == 'X' and b[7] == 'X':
                return 10
            if b[1] == 'O' and b[4] == 'O' and b[7] == 'O':
                return -10
            if b[2] == 'X' and b[5] == 'X' and b[8] == 'X':
                return 10
            if b[2] == 'O' and b[5] == 'O' and b[8] == 'O':
                return -10
            if b[0] == 'X' and b[4] == 'X' and b[8] == 'X':
                return 10
            if b[0] == 'O' and b[4] == 'O' and b[8] == 'O':
                return -10
            if b[2] == 'X' and b[4] == 'X' and b[6] == 'X':
                return 10
            if b[2] == 'O' and b[4] == 'O' and b[6] == 'O':
                return -10
            return 0

        def minmax(b, isAI):
            score = evaluate(b)
            if (score == 10):
                return score
            if (score == -10):
                return score
            if (isMovesLeft(b) == False):
                return 0

            if (isAI):
                best = -9999
                for i in range(9):
                    if b[i] =='-':
                        b[i] = 'X'
                        best = max(best, minmax(b, not isAI))
                        b[i] = '-'
                return best
            else:
                best = 9999
                for i in range(9):
                    if( b[i] =='-'):
                        b[i] = 'O'
                        best = min(best, minmax(b, not isAI))
                        b[i] = '-'
                return best

        def findBestMove(b):
            bestMoveValue = -9999
            bestMove = -1
            for i in range(9):
                if (b[i] == '-'):
                    b[i] = 'X'
                    moveValue = minmax(b, False)
                    b[i] = '-'
                    if (moveValue > bestMoveValue):
                        bestMove = i
                        bestMoveValue = moveValue
            return bestMove
        return (str(findBestMove(list(self.info))))
