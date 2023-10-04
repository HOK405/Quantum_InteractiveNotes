using InteractiveNotes.Data;
using InteractiveNotes.Data.Entities;
using InteractiveNotes.Data.Repositories;
using InteractiveNotes.DTO;
using Moq;

namespace InteractiveNotes.Tests.Services
{
    public class NoteServiceTests
    {
        private readonly Mock<INoteRepository> _mockRepo;
        private readonly NoteService _noteService;

        public NoteServiceTests()
        {
            _mockRepo = new Mock<INoteRepository>();
            _noteService = new NoteService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllNotesAsync_ShouldReturnListOfNoteDtos_WhenNotesExist()
        {
            // Arrange
            var notes = new List<Note> { new Note { NoteId = 1, Title = "Test", Content = "Test content", CreatingDate = DateTime.Now } };
            _mockRepo.Setup(repo => repo.GetAllNotesAsync()).ReturnsAsync(notes);

            // Act
            var result = await _noteService.GetAllNotesAsync();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<NoteDto>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllNotesAsync_ShouldThrowInvalidOperationException_WhenRepositoryReturnsNull()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllNotesAsync()).ReturnsAsync((IEnumerable<Note>)null);

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _noteService.GetAllNotesAsync());
        }


        [Fact]
        public async Task GetNoteByIdAsync_ShouldReturnNoteDto_WhenNoteExists()
        {
            // Arrange
            var note = new Note { NoteId = 1, Title = "Test", Content = "Test content", CreatingDate = DateTime.Now };
            _mockRepo.Setup(repo => repo.GetNoteByIdAsync(1)).ReturnsAsync(note);

            // Act
            var result = await _noteService.GetNoteByIdAsync(1);

            // Assert
            Assert.IsType<NoteDto>(result);
            Assert.Equal(1, result.NoteId);
        }

        [Fact]
        public async Task GetNoteByIdAsync_ShouldThrowKeyNotFoundException_WhenNoteNotFound()
        {
            // Arrange
            int nonExistentId = 999; // Assuming this ID does not exist in the repository.
            _mockRepo.Setup(repo => repo.GetNoteByIdAsync(nonExistentId)).ReturnsAsync((Note)null);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _noteService.GetNoteByIdAsync(nonExistentId));
        }


        [Fact]
        public async Task AddNoteAsync_ShouldAddNote()
        {
            // Arrange
            var noteDto = new NoteDto { NoteId = 1, Title = "Test", Content = "Test content", CreatingDate = DateTime.Now };

            // Act
            await _noteService.AddNoteAsync(noteDto);

            // Assert
            _mockRepo.Verify(repo => repo.AddNoteAsync(It.IsAny<Note>()), Times.Once);
        }

        [Fact]
        public async Task AddNoteAsync_ShouldThrowArgumentNullException_WhenNoteDtoIsNull()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _noteService.AddNoteAsync(null));
        }

        [Fact]
        public async Task UpdateNoteAsync_ShouldUpdateExistingNote()
        {
            // Arrange
            var noteDto = new NoteDto { NoteId = 1, Title = "Test", Content = "Test content", CreatingDate = DateTime.Now };
            var note = new Note { NoteId = 1, Title = "Test", Content = "Test content", CreatingDate = DateTime.Now };
            _mockRepo.Setup(repo => repo.GetNoteByIdAsync(1)).ReturnsAsync(note);

            // Act
            await _noteService.UpdateNoteAsync(noteDto);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateNoteAsync(It.IsAny<Note>()), Times.Once);
        }

        [Fact]
        public async Task UpdateNoteAsync_ShouldThrowArgumentNullException_WhenNoteDtoIsNull()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _noteService.UpdateNoteAsync(null));
        }

        [Fact]
        public async Task UpdateNoteAsync_ShouldThrowArgumentException_WhenNoteIdIsInvalid()
        {
            // Arrange
            var invalidNoteDto = new NoteDto { NoteId = 0 }; // Invalid Note ID

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _noteService.UpdateNoteAsync(invalidNoteDto));
        }

        [Fact]
        public async Task UpdateNoteAsync_ShouldThrowKeyNotFoundException_WhenNoteNotFound()
        {
            // Arrange
            int nonExistentNoteId = 999;
            var noteDto = new NoteDto { NoteId = nonExistentNoteId };

            // Setup the repository to return null for the non-existent note
            _mockRepo.Setup(repo => repo.GetNoteByIdAsync(nonExistentNoteId)).ReturnsAsync((Note)null);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _noteService.UpdateNoteAsync(noteDto));
        }


        [Fact]
        public async Task DeleteNoteAsync_ShouldDeleteNote_WhenNoteExists()
        {
            {
                // Arrange
                var note = new Note { NoteId = 1, Title = "Test", Content = "Test content", CreatingDate = DateTime.Now };
                _mockRepo.Setup(repo => repo.GetNoteByIdAsync(1)).ReturnsAsync(note);

                // Act
                await _noteService.DeleteNoteAsync(1);

                // Assert
                _mockRepo.Verify(repo => repo.DeleteNoteAsync(1), Times.Once);
            }
        }

        [Fact]
        public async Task DeleteNoteAsync_ShouldThrowArgumentException_WhenNoteIdIsInvalid()
        {
            // Arrange
            int invalidNoteId = 0; // Invalid Note ID

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _noteService.DeleteNoteAsync(invalidNoteId));
        }

        [Fact]
        public async Task DeleteNoteAsync_ShouldThrowKeyNotFoundException_WhenNoteNotFound()
        {
            // Arrange
            int nonExistentId = 999; 
            _mockRepo.Setup(repo => repo.GetNoteByIdAsync(nonExistentId)).ReturnsAsync((Note)null);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _noteService.DeleteNoteAsync(nonExistentId));
        }
    }
}
